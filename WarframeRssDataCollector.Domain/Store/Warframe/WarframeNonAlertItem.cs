using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarframeRssDataCollector.Domain.Store.Warframe
{
    public class WarframeNonAlertItem : WarframeItem
    {

    //    Guid { get; set; }
    //public string Title { get; set; }
    //public string Author { get; set; }
    //public DateTime PubDate { get; set; }

    public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Environment.NewLine);
            sb.Append("AUTHOR: " + Author + Environment.NewLine);
            sb.Append("TITLE: " + Title);

            return sb.ToString();
        }

        public bool isValid()
        {
            if ((Guid.ToLower().Contains("N/A"))    ||      (Guid == null))     { return false; }
            if ((Author.ToLower().Contains("N/A"))  ||      (Author == null))   { return false; }
            if ((Title.ToLower().Contains("N/A"))   ||      (Title == null))    { return false; }
            if (PubDate == null) { return false; }

            return true;
        }
    }
}
