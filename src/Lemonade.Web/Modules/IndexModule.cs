using Nancy;

namespace Lemonade.Web.Modules
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = p => View["Index"];
        }
    }
}