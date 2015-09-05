using System.Web.Mvc;

namespace Lemonade
{
    public class FeatureAttribute : ActionFilterAttribute
    {
        public FeatureAttribute(string featureName)
        {
            _featureName = featureName;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!Feature.Switches[_featureName]) filterContext.Result = new EmptyResult();
            base.OnActionExecuting(filterContext);
        }

        private readonly string _featureName;
    }
}