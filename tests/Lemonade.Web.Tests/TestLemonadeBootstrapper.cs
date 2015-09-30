using System;
using System.Collections.Generic;
using Lemonade.Core.Commands;
using Lemonade.Core.Queries;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Queries;
using Lemonade.Web.Infrastructure;
using Nancy.TinyIoc;

namespace Lemonade.Web.Tests
{
    public class TestLemonadeBootstrapper : LemonadeBootstrapper
    {
        public void ConfigureDependency(Action<TinyIoCContainer> configuration)
        {
            _additionalConfigurations.Add(configuration);
        }

        protected override void ConfigureDependencies(TinyIoCContainer container)
        {
            container.Register<IGetAllFeatures, GetAllFeatures>();
            container.Register<IGetFeatureByNameAndApplication, GetFeatureByNameAndApplication>();
            container.Register<ISaveFeature, SaveFeature>();
            container.Register<IDeleteApplication, DeleteApplication>();

            foreach (var additionalConfiguration in _additionalConfigurations)
            {
                additionalConfiguration(container);
            }
        }

        private readonly List<Action<TinyIoCContainer>> _additionalConfigurations = new List<Action<TinyIoCContainer>>();
    }
}