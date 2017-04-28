using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using WarframeRssDataCollector.Domain;
using WarframeRssDataCollector.Domain.Store.Result;
using WarframeRssDataCollector.Domain.Store.Warframe;

namespace WarframeRssDataCollector.Data.Functional
{
    public class getRssData
    {
        private async Task<ResultBase<rss>> DeserializeRSSFeedAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    WebRequest request = WebRequest.Create("http://content.warframe.com/dynamic/rss.php");
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);

                    XmlSerializer serializer = new XmlSerializer(typeof(rss));
                    StringReader rdr = new StringReader(reader.ReadToEnd());

                    rss result = (rss)serializer.Deserialize(new XmlTextReader(rdr));

                    return new ResultBase<rss>(result, true, null);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error while getting WebResponse (This error is too generic)");
                    Console.WriteLine(ex);
                    return new ResultBase<rss>(null, true, ex.ToString());
                }
            });
        }

        public List<WarframeItem> getData()
        {
            ResultBase<rss> RSSFeed = DeserializeRSSFeedAsync().Result;
            if (!RSSFeed.Success) return new List<WarframeItem>();

            List<WarframeItem> data = new List<WarframeItem>();

            foreach(rssChannelItem item in RSSFeed.Result.channel.item)
            {
                string Guid = "";
                string Title = "";
                string Author = "";
                string PubDate = "";
                DateTime PublishDate;
                string Desc = "";
                string Faction = "";
                string Expiry = "";
                DateTime ExpiryDate;

                WarframeDateConversions converter = new WarframeDateConversions();

                int count = 0;
                foreach (ItemsChoiceType attr in item.ItemsElementName)
                {
                    string temp = attr.ToString();
                    if (temp.ToLower().Contains("guid"))    { Guid = item.Items[count]; }
                    if (temp.ToLower().Contains("title"))   { Title = item.Items[count]; }
                    if (temp.ToLower().Contains("author"))  { Author = item.Items[count]; }
                    if (temp.ToLower().Contains("pubdate")) { PubDate = item.Items[count]; }
                    if (temp.ToLower().Contains("desc"))    { Desc = item.Items[count]; }
                    if (temp.ToLower().Contains("faction")) { Faction = item.Items[count]; }
                    if (temp.ToLower().Contains("expiry"))  { Expiry = item.Items[count]; }
                    count++;
                }

                PublishDate = converter.getDate(PubDate);

                if (Author == "Alert")
                {
                    ExpiryDate = converter.getDate(Expiry);
                    data.Add(new WarframeAlertItem()
                    {
                        Guid = Guid,
                        Title = Title,
                        Author = Author,
                        PubDate = PublishDate,
                        Description = Desc,
                        Faction = Faction,
                        Expiry = ExpiryDate
                    });
                }

                if (Author == "Invasion")
                {
                    data.Add(new WarframeNonAlertItem()
                    {
                        Guid = Guid,
                        Title = Title,
                        Author = Author,
                        PubDate = PublishDate
                    });
                }

                if (Author == "Outbreak")
                {
                    data.Add(new WarframeNonAlertItem()
                    {
                        Guid = Guid,
                        Title = Title,
                        Author = Author,
                        PubDate = PublishDate
                    });
                }
            }
            
            Console.WriteLine("Refresh " + DateTime.Now.ToString("HH:mm:ss"));

            return data;
        }

        public List<WarframeItem> compare(List<WarframeItem> current, List<WarframeItem> old)
        {
            List<WarframeItem> diff = new List<WarframeItem>();

            if(old != null)
            {
                foreach (WarframeItem currentItem in current)
                {
                    bool Found = false;
                    foreach (WarframeItem oldItem in old)
                    {
                        if (currentItem.Guid == oldItem.Guid)
                        {
                            Found = true;
                        }
                    }

                    if (!Found)
                    {
                        diff.Add(currentItem);
                        Console.WriteLine("Item Found " + currentItem.ToString());
                    }
                }

                //if(diff.Count() > 0)
                //    Console.WriteLine("Items Found " + diff.Count());
            }
            else  //TODO get rid of this \/\/\/
            {
                diff = current;
            }
            
            return diff;
        }
    }
}
