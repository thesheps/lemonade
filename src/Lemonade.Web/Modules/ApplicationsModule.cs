using System.Linq;
using Lemonade.Web.Mappers;
using Nancy;
using Nancy.ModelBinding;
using System.Collections.Generic;
using Lemonade.Data.Commands;
using Lemonade.Data.Exceptions;
using Lemonade.Data.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Events;

namespace Lemonade.Web.Modules
{
    public class ApplicationsModule : NancyModule
    {
        public ApplicationsModule(IGetAllApplications getAllApplications, ICreateApplication createApplication, IUpdateApplication updateApplication, IDeleteApplication deleteApplication)
        {
            _getAllApplications = getAllApplications;
            _createApplication = createApplication;
            _updateApplication = updateApplication;
            _deleteApplication = deleteApplication;

            Get["/api/applications"] = p => GetApplications();
            Post["/api/applications"] = p => PostApplication();
            Put["/api/applications"] = p => PutApplication();
            Delete["/api/applications"] = p => DeleteApplication();
        }

        private IList<Application> GetApplications()
        {
            return _getAllApplications.Execute().Select(a => a.ToContract()).ToList();
        }

        private HttpStatusCode PostApplication()
        {
            try
            {
                var application = this.Bind<Application>().ToEntity();
                _createApplication.Execute(application);
                DomainEvent.Raise(new ApplicationHasBeenCreated(application.ApplicationId, application.Name));

                return HttpStatusCode.OK;
            }
            catch (CreateApplicationException exception)
            {
                DomainEvent.Raise(new ApplicationErrorHasOccurred(exception.Message));
                return HttpStatusCode.BadRequest;
            }
        }

        private HttpStatusCode PutApplication()
        {
            try
            {
                var application = this.Bind<Application>();
                _updateApplication.Execute(application.ToEntity());
                DomainEvent.Raise(new ApplicationHasBeenUpdated(application.ApplicationId, application.Name));

                return HttpStatusCode.OK;
            }
            catch (UpdateApplicationException exception)
            {
                DomainEvent.Raise(new ApplicationErrorHasOccurred(exception.Message));
                return HttpStatusCode.BadRequest;
            }
        }

        private HttpStatusCode DeleteApplication()
        {
            int applicationId;
            int.TryParse(Request.Query["id"].Value as string, out applicationId);

            try
            {
                _deleteApplication.Execute(applicationId);
                DomainEvent.Raise(new ApplicationHasBeenDeleted(applicationId));
                return HttpStatusCode.OK;
            }
            catch (DeleteApplicationException exception)
            {
                DomainEvent.Raise(new ApplicationErrorHasOccurred(exception.Message));
                return HttpStatusCode.BadRequest;
            }
        }

        private readonly IGetAllApplications _getAllApplications;
        private readonly ICreateApplication _createApplication;
        private readonly IUpdateApplication _updateApplication;
        private readonly IDeleteApplication _deleteApplication;
    }
}