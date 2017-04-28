using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarframeRssDataCollector.Domain.Store.Warframe
{
    public class WarframeAlertItem : WarframeItem
    {
        public string Description { get; set; }
        public string Faction { get; set; }
        public DateTime Expiry { get; set; }

        public override string ToString()
        {
            //StringBuilder sb = new StringBuilder();
            //sb.Append(Environment.NewLine);
            //sb.Append("GUID: " + Guid + Environment.NewLine);
            //sb.Append("AUTHOR: " + Author + Environment.NewLine);
            //sb.Append("TITLE: " + Title + Environment.NewLine);

            //sb.Append("DESC: " + Description + Environment.NewLine);
            //sb.Append("FACTION: " + Faction + Environment.NewLine);
            //sb.Append("PUBDATE: " + PubDate + Environment.NewLine);
            //sb.Append("EXPIRY: " + Expiry + Environment.NewLine);

            StringBuilder sb = new StringBuilder();
            sb.Append(Environment.NewLine);
            sb.Append("AUTHOR: " + Author + Environment.NewLine);
            sb.Append("TITLE: " + Title);

            return sb.ToString();
        }

        public bool isValid()
        {
            if ((Guid.ToLower().Contains("N/A"))        ||      (Guid == null))         { return false; }
            if ((Author.ToLower().Contains("N/A"))      ||      (Author == null))       { return false; }
            if ((Title.ToLower().Contains("N/A"))       ||      (Title == null))        { return false; }
            if ((Description.ToLower().Contains("N/A")) ||      (Description == null))  { return false; }
            if ((Faction.ToLower().Contains("N/A"))     ||      (Faction == null))      { return false; }
            if (PubDate == null) { return false; }
            if (Expiry == null) { return false; }

            return true;
        }
    }
}
