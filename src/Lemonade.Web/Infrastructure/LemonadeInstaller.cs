using System;
using System.Collections.Generic;
using System.Linq;
using Lemonade.Web.Core.CommandHandlers;
using Lemonade.Web.Core.EventHandlers;
using Lemonade.Web.Core.QueryHandlers;
using Lemonade.Web.Core.Services;
using Lemonade.Web.Services;
using Nancy.TinyIoc;

namespace Lemonade.Web.Infrastructure
{
    public class LemonadeInstaller
    {
        public static void Install(TinyIoCContainer container)
        {
            container.Register<INotifyClients, NotifyClients>();
            InstallGenerics(container, typeof(IQueryHandler<,>));
            InstallGenerics(container, typeof(IDomainEventHandler<>));
            InstallGenerics(container, typeof(ICommandHandler<>));
        }

        private static void InstallGenerics(TinyIoCContainer container, Type genericType)
        {
            foreach (var type in Types)
            {
                type.GetInterfaces()
                    .Where(t => t.IsGenericType && !t.ContainsGenericParameters && genericType.IsAssignableFrom(t.GetGenericTypeDefinition()))
                    .ToList()
                    .ForEach(t => container.Register(t, type));
            }
        }

        private static readonly IList<Type> Types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()).ToList();
    }
}