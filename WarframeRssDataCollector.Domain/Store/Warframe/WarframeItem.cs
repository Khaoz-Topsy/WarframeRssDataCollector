using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarframeRssDataCollector.Domain.Store.Warframe
{
    public abstract class WarframeItem
    {
        public string Guid { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PubDate { get; set; }

        public override string ToString()
        {
            return "base class";
        }
    }
}
