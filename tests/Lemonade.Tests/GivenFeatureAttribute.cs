using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Lemonade.Resolvers;
using NSubstitute;
using NUnit.Framework;

namespace Lemonade.Resolvers
{
    public class GivenFeatureAttribute
    {
        [Test]
        public void WhenFeatureSwitchedOff_ThenEmptyResults()
        {
            Lemonade.FeatureResolver = new AppConfigFeatureResolver();
            var filterAttribute = new FeatureAttribute("DisabledProperty");
            var request = Substitute.For<HttpRequestBase>();
            request.ContentType.Returns("application/json");

            var httpContext = Substitute.For<HttpContextBase>();
            httpContext.Request.Returns(request);

            var routeData = new RouteData();
            routeData.Values.Add("employeeId", "123");

            var context = Substitute.For<ActionExecutingContext>();
            context.HttpContext.Returns(httpContext);
            filterAttribute.OnActionExecuting(context);
            
            Assert.That(context.Result, Is.Not.Null);
        }
    }
}