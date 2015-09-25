using System;
using System.Collections.Generic;
using Lemonade.Core.Commands;
using Lemonade.Core.Queries;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Queries;
using Nancy.TinyIoc;

namespace Lemonade.Web.Tests
{
    public class FakeLemonadeBootstrapper : LemonadeBootstrapper
    {
        public FakeLemonadeBootstrapper()
        {
            _additionalConfigurations = new List<Action<TinyIoCContainer>>();
        }

        protected override void ConfigureDependencies(TinyIoCContainer container)
        {
            _container = container;
            container.Register<IGetAllFeatures, GetAllFeatures>();
            container.Register<IGetFeatureByNameAndApplication, GetFeatureByNameAndApplication>();
            container.Register<ISaveFeature, SaveFeature>();
            container.Register<IDeleteApplication, DeleteApplication>();

            foreach (var additionalConfiguration in _additionalConfigurations)
            {
                additionalConfiguration(container);
            }
        }

        public void ConfigureAdditionalDependencies(Action<TinyIoCContainer> configuration)
        {
            _additionalConfigurations.Add(configuration);
        }


        private TinyIoCContainer _container;
        private List<Action<TinyIoCContainer>> _additionalConfigurations;
    }
}