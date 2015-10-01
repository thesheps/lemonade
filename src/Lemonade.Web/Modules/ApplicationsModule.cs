﻿using System.Linq;
using Lemonade.Web.Mappers;
using Nancy;
using Nancy.ModelBinding;
using System.Collections.Generic;
using Lemonade.Core.Commands;
using Lemonade.Core.Events;
using Lemonade.Core.Exceptions;
using Lemonade.Core.Queries;
using Lemonade.Web.Contracts;

namespace Lemonade.Web.Modules
{
    public class ApplicationsModule : NancyModule
    {
        public ApplicationsModule(IGetAllApplications getAllApplications, ISaveApplication saveApplication, IDeleteApplication deleteApplication)
        {
            _getAllApplications = getAllApplications;
            _saveApplication = saveApplication;
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
                _saveApplication.Execute(this.Bind<Application>().ToDomain());
                return HttpStatusCode.OK;
            }
            catch (SaveApplicationException exception)
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
                return HttpStatusCode.OK;
            }
            catch (DeleteApplicationException exception)
            {
                DomainEvent.Raise(new ErrorHasOccurred(exception.Message));
                return HttpStatusCode.BadRequest;
            }
        }

        private readonly IGetAllApplications _getAllApplications;
        private readonly ISaveApplication _saveApplication;
        private readonly IDeleteApplication _deleteApplication;
    }
}