using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using WarframeRssDataCollector.Data.Repositories;
using WarframeRssDataCollector.Domain;
using WarframeRssDataCollector.Domain.Store.Result;
using WarframeRssDataCollector.Domain.Store.Warframe;

namespace WarframeRssDataCollector.Data.Functional
{
    public class getRssData
    {
        private ResultBase<rss> OldRSSFeed = new ResultBase<rss>(new rss(), false, "");
        private ResultBase<rss> CurrentRSSFeed = new ResultBase<rss>(new rss(), false, "");

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
                catch (InvalidCastException ex)
                {
                    return writeMsg("Error while getting WebResponse, InvalidOperationException??", ex.ToString(), false);
                }
                catch (InvalidOperationException ex)
                {
                    return writeMsg("InvalidCastException. Means ISP is down and sending a login page, which is technically a http 200", ex.ToString(), false);
                }
                catch (NotImplementedException ex)
                {
                    return writeMsg("Error while getting WebResponse, NotImplementedException??", ex.ToString(), true);
                }
                catch (ArgumentNullException ex)
                {
                    return writeMsg("Error while getting WebResponse, ArgumentNullException??", ex.ToString(), true);
                }
                catch (ArgumentException ex)
                {
                    return writeMsg("Error while getting WebResponse, ArgumentException??", ex.ToString(), true);
                }
                catch (Exception ex)
                {
                    return writeMsg("Error while getting WebResponse, generel exception", ex.ToString(), true);
                }
            });
        }

        public List<WarframeItem> getData()
        {
            OldRSSFeed = CurrentRSSFeed;
            CurrentRSSFeed = DeserializeRSSFeedAsync().Result;
            List<WarframeItem> data = new List<WarframeItem>();

            if (!CurrentRSSFeed.Success) return data;
            if (CurrentRSSFeed.Result.channel == null) { return data; }

            #region ForeachItem
            foreach(rssChannelItem item in CurrentRSSFeed.Result.channel.item)
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
            #endregion

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
            }
            else  //TODO get rid of this \/\/\/
            {
                diff = current;
            }
            
            return diff;
        }

        private ResultBase<rss> writeMsg(string message, string ex, bool sendViaPush = false)
        {
            if (sendViaPush) { sendPushJet(message); }

            Console.WriteLine(message);
            //Console.WriteLine(ex);

            return new ResultBase<rss>(OldRSSFeed.Result, true, ex.ToString());
        }

        private async void sendPushJet(string message)
        {
            PushJetRepository PushJetRepo = new PushJetRepository();
            PushJetRepo.SendMessage(new PushJetMessageContent()
            {
                secret = SecretData.PushJetHomeSecret,
                title = SecretData.PushJetHomeServiceName,
                level = 5,
                message = message
            });
        }
    }
}
