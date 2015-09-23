using Lemonade.Core.Entities;

namespace Lemonade.Builders
{
    public class ApplicationBuilder
    {
        public ApplicationBuilder()
        {
            _application = new Application();
        }

        public ApplicationBuilder WithName(string applicationName)
        {
            _application.Name = applicationName;
            return this;
        }

        public Application Build()
        {
            return _application;
        }

        private readonly Application _application;
    }
}