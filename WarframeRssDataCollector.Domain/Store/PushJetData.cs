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

        public PushJetMessageContent Clone()
        {
            PushJetMessageContent newMsg = new PushJetMessageContent();
            if (!String.IsNullOrEmpty(this.secret))     { newMsg.secret = this.secret; }
            if (!String.IsNullOrEmpty(this.message))    { newMsg.message = this.message; }
            if (!String.IsNullOrEmpty(this.title))      { newMsg.title = this.title; }
            if (level > 0)                              { newMsg.level = this.level; }
            if (!String.IsNullOrEmpty(this.link))       { newMsg.link = this.link; }

            return newMsg;
        }
    }
}
