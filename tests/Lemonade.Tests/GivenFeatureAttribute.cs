using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Lemonade.Services;
using Lemonade.Tests.Fakes;
using Moq;
using NUnit.Framework;

namespace Lemonade.Tests
{
    public class GivenFeatureAttribute
    {
        [Test]
        public void WhenFeatureSwitchedOff_ThenEmptyResults()
        {
            Feature.SetResolver(new AppConfigFeatureResolver());
            var filterAttribute = new FeatureAttribute("DisabledProperty");
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(r => r.ContentType).Returns("application/json");

            var httpContext = new Mock<HttpContextBase>();
            httpContext.SetupGet(c => c.Request).Returns(request.Object);

            var routeData = new RouteData();
            routeData.Values.Add("employeeId", "123");

            var context = new Mock<ActionExecutingContext>();
            context.SetupGet<HttpContextBase>(c => c.HttpContext).Returns(httpContext.Object);
            filterAttribute.OnActionExecuting(context.Object);
            
            Assert.That(context.Object.Result, Is.Not.Null);
        }
    }
}