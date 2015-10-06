﻿using System.Collections.Generic;
using System.Linq;
using Lemonade.Core.Commands;
using Lemonade.Core.Events;
using Lemonade.Core.Exceptions;
using Lemonade.Core.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Mappers;
using Nancy;
using Nancy.ModelBinding;

namespace Lemonade.Web.Modules
{
    public class FeaturesModule : NancyModule
    {
        public FeaturesModule(IGetFeatureByNameAndApplication getFeatureByNameAndApplication, IGetApplicationByName getApplicationByName, IGetAllFeaturesByApplicationId getAllFeaturesByApplicationId,
            ISaveFeature saveFeature, IDeleteFeature deleteFeature, ISaveApplication saveApplication)
        {
            _getFeatureByNameAndApplication = getFeatureByNameAndApplication;
            _getApplicationByName = getApplicationByName;
            _getAllFeaturesByApplicationId = getAllFeaturesByApplicationId;
            _saveFeature = saveFeature;
            _deleteFeature = deleteFeature;
            _saveApplication = saveApplication;

            Post["/api/features"] = p => CreateFeature();
            Put["/api/features"] = p => UpdateFeature();
            Get["/api/features"] = p => GetFeatures();
            Get["/api/feature"] = p => GetFeature();
            Get["/features"] = p => View["Features"];
            Delete["/api/features"] = p => DeleteFeature();
        }

        private dynamic UpdateFeature()
        {
            throw new System.NotImplementedException();
        }

        private HttpStatusCode CreateFeature()
        {
            try
            {
                _saveFeature.Execute(this.Bind<Feature>().ToDomain());
                return HttpStatusCode.OK;
            }
            catch (SaveFeatureException exception)
            {
                DomainEvent.Raise(new ErrorHasOccurred(exception.Message));
                return HttpStatusCode.BadRequest;
            }
        }

        private Feature GetFeature()
        {
            var featureName = Request.Query["feature"].Value as string;
            var applicationName = Request.Query["application"].Value as string;
            var feature = _getFeatureByNameAndApplication.Execute(featureName, applicationName);

            if (feature != null) return feature.ToContract();

            var application = GetApplication(applicationName);
            feature = new Core.Domain.Feature { Name = featureName, ApplicationId = application.ApplicationId, Application = application };
            _saveFeature.Execute(feature);

            return feature.ToContract();
        }

        private Core.Domain.Application GetApplication(string applicationName)
        {
            var application = _getApplicationByName.Execute(applicationName);
            if (application != null) return application;

            application = new Core.Domain.Application { Name = applicationName };
            _saveApplication.Execute(application);

            return application;
        }

        private IList<Feature> GetFeatures()
        {
            int applicationId;
            int.TryParse(Request.Query["applicationId"].Value as string, out applicationId);

            var feature = _getAllFeaturesByApplicationId.Execute(applicationId);

            return feature.Select(f => f.ToContract()).ToList();
        }

        private dynamic DeleteFeature()
        {
            int featureId;
            int.TryParse(Request.Query["id"].Value as string, out featureId);

            try
            {
                _deleteFeature.Execute(featureId);
                return HttpStatusCode.OK;
            }
            catch (DeleteFeatureException exception)
            {
                DomainEvent.Raise(new ErrorHasOccurred(exception.Message));
                return HttpStatusCode.BadRequest;
            }
        }

        private readonly IGetFeatureByNameAndApplication _getFeatureByNameAndApplication;
        private readonly IGetApplicationByName _getApplicationByName;
        private readonly IGetAllFeaturesByApplicationId _getAllFeaturesByApplicationId;
        private readonly ISaveFeature _saveFeature;
        private readonly IDeleteFeature _deleteFeature;
        private readonly ISaveApplication _saveApplication;
    }
}