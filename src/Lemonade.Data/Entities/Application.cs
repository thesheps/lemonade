using System.Collections.Generic;

namespace Lemonade.Core.Entities
{
    public class Application
    {
        public int ApplicationId { get; set; }
        public string Name { get; set; }
        public List<Feature> Features { get; set; }
    }
}