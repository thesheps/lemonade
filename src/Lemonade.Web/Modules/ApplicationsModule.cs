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
using Lemonade.Web.Infrastructure;

namespace Lemonade.Web.Modules
{
    public class ApplicationsModule : NancyModule
    {
        public ApplicationsModule(IDomainEventDispatcher eventDispatcher, IGetAllApplications getAllApplications, ICreateApplication createApplication, IUpdateApplication updateApplication, IDeleteApplication deleteApplication)
        {
            _eventDispatcher = eventDispatcher;
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
                _eventDispatcher.Dispatch(new ApplicationHasBeenCreated(application.ApplicationId, application.Name));

                return HttpStatusCode.OK;
            }
            catch (CreateApplicationException exception)
            {
                _eventDispatcher.Dispatch(new ApplicationErrorHasOccurred(exception.Message));
                return HttpStatusCode.BadRequest;
            }
        }

        private HttpStatusCode PutApplication()
        {
            try
            {
                var application = this.Bind<Application>();
                _updateApplication.Execute(application.ToEntity());
                _eventDispatcher.Dispatch(new ApplicationHasBeenUpdated(application.ApplicationId, application.Name));

                return HttpStatusCode.OK;
            }
            catch (UpdateApplicationException exception)
            {
                _eventDispatcher.Dispatch(new ApplicationErrorHasOccurred(exception.Message));
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
                _eventDispatcher.Dispatch(new ApplicationHasBeenDeleted(applicationId));
                return HttpStatusCode.OK;
            }
            catch (DeleteApplicationException exception)
            {
                _eventDispatcher.Dispatch(new ApplicationErrorHasOccurred(exception.Message));
                return HttpStatusCode.BadRequest;
            }
        }

        private readonly IDomainEventDispatcher _eventDispatcher;
        private readonly IGetAllApplications _getAllApplications;
        private readonly ICreateApplication _createApplication;
        private readonly IUpdateApplication _updateApplication;
        private readonly IDeleteApplication _deleteApplication;
    }
}