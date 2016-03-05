using System.Linq;
using Lemonade.Web.Mappers;
using Nancy;
using Nancy.ModelBinding;
using System.Collections.Generic;
using Lemonade.Data.Exceptions;
using Lemonade.Data.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Core.Commands;
using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Modules
{
    public class ApplicationsModule : NancyModule
    {
        public ApplicationsModule(IDomainEventDispatcher eventDispatcher, ICommandDispatcher commandDispatcher, IGetAllApplications getAllApplications)
        {
            _eventDispatcher = eventDispatcher;
            _commandDispatcher = commandDispatcher;
            _getAllApplications = getAllApplications;

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
                _commandDispatcher.Dispatch(new CreateApplicationCommand(this.Bind<Application>().Name));
                return HttpStatusCode.OK;
            }
            catch (CreateApplicationException)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        private HttpStatusCode PutApplication()
        {
            try
            {
                var application = this.Bind<Application>();
                _commandDispatcher.Dispatch(new UpdateApplicationCommand(application.ApplicationId, application.Name));

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
                _commandDispatcher.Dispatch(new DeleteApplicationCommand(applicationId));
                return HttpStatusCode.OK;
            }
            catch (DeleteApplicationException)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        private readonly IDomainEventDispatcher _eventDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IGetAllApplications _getAllApplications;
    }
}