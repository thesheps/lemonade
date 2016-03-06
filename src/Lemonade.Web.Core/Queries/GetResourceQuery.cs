using Lemonade.Web.Contracts;

namespace Lemonade.Web.Core.Queries
{
    public class GetResourceQuery : IQuery<Resource>
    {
        public string Application { get; }
        public string ResourceSet { get; }
        public string ResourceKey { get; }
        public string Locale { get; }

        public GetResourceQuery(string application, string resourceSet, string resourceKey, string locale)
        {
            Application = application;
            ResourceSet = resourceSet;
            ResourceKey = resourceKey;
            Locale = locale;
        }
    }
}