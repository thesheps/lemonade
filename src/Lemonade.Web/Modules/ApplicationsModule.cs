using System.Linq;
using Lemonade.Web.Mappers;
using Nancy;
using Nancy.ModelBinding;
using System.Collections.Generic;
using Lemonade.Core.Commands;
using Lemonade.Core.Exceptions;
using Lemonade.Core.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Events;

namespace Lemonade.Web.Modules
{
    public class ApplicationsModule : NancyModule
    {
        public ApplicationsModule(IGetAllApplications getAllApplications, ICreateApplication createApplication, IDeleteApplication deleteApplication)
        {
            _getAllApplications = getAllApplications;
            _createApplication = createApplication;
            _deleteApplication = deleteApplication;

            Get["/api/applications"] = p => GetApplications();
            Post["/api/applications"] = p => PostApplication();
            Delete["/api/applications"] = p => DeleteApplication();
        }

        private IList<Application> GetApplications()
        {
            return _getAllApplications.Execute().Select(a => a.ToContract()).ToList();
        }

        private dynamic PostApplication()
        {
            try
            {
                var application = this.Bind<Application>();
                _createApplication.Execute(application.ToDomain());
                DomainEvent.Raise(new ApplicationHasBeenCreated(application.ApplicationId, application.Name));

                return HttpStatusCode.OK;
            }
            catch (CreateApplicationException exception)
            {
                DomainEvent.Raise(new ErrorHasOccurred(exception.Message));
                return HttpStatusCode.BadRequest;
            }
        }

        private dynamic DeleteApplication()
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
                DomainEvent.Raise(new ErrorHasOccurred(exception.Message));
                return HttpStatusCode.BadRequest;
            }
        }

        private readonly IGetAllApplications _getAllApplications;
        private readonly ICreateApplication _createApplication;
        private readonly IDeleteApplication _deleteApplication;
    }
}