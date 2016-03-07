using Lemonade.Data.Entities;

namespace Lemonade.Builders
{
    public class ResourceBuilder
    {
        public ResourceBuilder WithResourceSet(string resourceSet)
        {
            _resource.ResourceSet = resourceSet;
            return this;
        }

        public ResourceBuilder WithResourceKey(string resourceKey)
        {
            _resource.ResourceKey = resourceKey;
            return this;
        }

        public ResourceBuilder WithLocale(Locale locale)
        {
            _resource.LocaleId = locale.LocaleId;
            _resource.Locale = locale;
            return this;
        }

        public ResourceBuilder WithValue(string value)
        {
            _resource.Value = value;
            return this;
        }

        public ResourceBuilder WithApplication(Application application)
        {
            _resource.ApplicationId = application.ApplicationId;
            _resource.Application = application;
            return this;
        }

        public Resource Build()
        {
            return _resource;
        }

        private readonly Resource _resource = new Resource();
    }
}