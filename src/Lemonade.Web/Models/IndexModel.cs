using System.Collections.Generic;

namespace Lemonade.Web.Models
{
    public class IndexModel
    {
        public int ApplicationId { get; set; }
        public IList<ApplicationModel> Applications { get; set; }
        public IList<FeatureModel> Features { get; set; } 
    }
}