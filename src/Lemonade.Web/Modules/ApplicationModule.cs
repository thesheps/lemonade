using Lemonade.Data.Commands;
using Lemonade.Data.Exceptions;
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

        private Response PostApplication()
        {
            try
            {
                _saveApplication.Execute(this.Bind<ApplicationModel>().ToEntity());
            }
            catch (SaveApplicationException exception)
            {
                ModelValidationResult.Errors.Add("SaveException", exception.Message);
            }

            return Response.AsRedirect("/feature");
        }

        private readonly ISaveApplication _saveApplication;
    }
}