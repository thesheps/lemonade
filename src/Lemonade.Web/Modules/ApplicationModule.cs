using Lemonade.Data.Commands;
using Lemonade.Web.Mappers;
using Lemonade.Web.Models;
using Nancy;
using Nancy.ModelBinding;

namespace Lemonade.Web.Modules
{
    public class ApplicationModule : NancyModule
    {
        public ApplicationModule(ISaveApplication saveApplication)
        {
            _saveApplication = saveApplication;

            Post["/application"] = p => PostApplication();
        }

        private HttpStatusCode PostApplication()
        {
            _saveApplication.Execute(this.Bind<ApplicationModel>().ToEntity());
            return HttpStatusCode.OK;
        }

        private readonly ISaveApplication _saveApplication;
    }
}