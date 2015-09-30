using System;
using System.Collections.Generic;
using System.Reflection;
using Nancy.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Lemonade.Web.Infrastructure
{
    public class CamelCasePropertyNameContractResolver : DefaultContractResolver
    {
        public HashSet<Assembly> AssembliesToInclude { get; set; }

        public CamelCasePropertyNameContractResolver()
        {
            AssembliesToInclude = new HashSet<Assembly>();
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jsonProperty = base.CreateProperty(member, memberSerialization);
            var declaringType = member.DeclaringType;

            if (AssembliesToInclude.Contains(declaringType.Assembly)) jsonProperty.PropertyName = jsonProperty.PropertyName.ToCamelCase();

            return jsonProperty;
        }
    }
}