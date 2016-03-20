using System;
using System.Collections.Generic;
using System.Linq;
using Nancy.TinyIoc;

namespace Lemonade.Web.Infrastructure
{
    public static class LemonadeInstaller
    {
        public static void RegisterImplementations(this TinyIoCContainer container, Type genericType)
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