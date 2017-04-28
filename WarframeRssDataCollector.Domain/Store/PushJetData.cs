using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarframeRssDataCollector.Domain
{
    public class PushJetMessageContent
    {
        public string secret { get; set; }
        public string message { get; set; }
        public string title { get; set; }
        public int level { get; set; }
        public string link { get; set; }
    }
}
