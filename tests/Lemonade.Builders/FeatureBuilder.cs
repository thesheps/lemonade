﻿using Lemonade.Data.Entities;

namespace Lemonade.Builders
{
    public class FeatureBuilder
    {
        public FeatureBuilder()
        {
            _feature = new Feature();
        }

        public FeatureBuilder WithName(string name)
        {
            _feature.Name = name;
            return this;
        }

        public FeatureBuilder WithIsEnabled(bool isEnabled)
        {
            _feature.IsEnabled = isEnabled;
            return this;
        }

        public FeatureBuilder WithApplication(Application application)
        {
            _feature.ApplicationId = application.ApplicationId;
            _feature.Application = application;
            return this;
        }

        public Feature Build()
        {
            return _feature;
        }

        private readonly Feature _feature;
    }
}