using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarframeRssDataCollector.Data.DI.Interface;
using WarframeRssDataCollector.Data.Functional;
using WarframeRssDataCollector.Data.Repositories.Interface;
using WarframeRssDataCollector.Domain;
using WarframeRssDataCollector.Domain.Store.Warframe;

namespace WarframeRssDataCollector.Data.Repositories
{
    public class MultiRepository : IBaseRepository
    {
        private IConnectionString connStr;
        private DatabaseRepository DbRepo;
        private PushJetRepository PushJetRepo;

        private PushJetMessageContent PushJetMsg;
        private StringBuilder msgContent;

        public MultiRepository(IConnectionString NinJectConnString)
        {
            connStr = NinJectConnString;
            DbRepo = new DatabaseRepository(connStr);
            PushJetRepo = new PushJetRepository();

            InitializePushJetMsg();
        }

        private void InitializePushJetMsg()
        {
            PushJetMsg = new PushJetMessageContent();
            PushJetMsg.secret = SecretData.PushJetSecret;
            PushJetMsg.title = "Khaoz Warframe Data";
            PushJetMsg.level = 3;

            msgContent = new StringBuilder();
        }

        private StringBuilder appendToString(WarframeItem wi)
        {
            if (msgContent.ToString().Length > 0)
                msgContent.Append("\n\n" + wi.Author.ToUpper() + " - " + wi.Title);
            else
                msgContent.Append(wi.Author.ToUpper() + " - " + wi.Title);

            return msgContent;
        }

        public void deleteFiles()
        {
        }

        public void saveAlerts(List<WarframeAlertItem> Alerts)
        {
            foreach(WarframeAlertItem a in Alerts)
            { msgContent = appendToString(a); }

            DbRepo.saveAlerts(Alerts);
        }

        public void saveInvasions(List<WarframeNonAlertItem> Invasions)
        {
            foreach (WarframeNonAlertItem i in Invasions)
            { msgContent = appendToString(i); }

            DbRepo.saveInvasions(Invasions);
        }

        public void saveOutbreaks(List<WarframeNonAlertItem> Outbreaks)
        {
            foreach (WarframeNonAlertItem o in Outbreaks)
            { msgContent = appendToString(o); }

            DbRepo.saveOutbreaks(Outbreaks);
        }

        public void flush()
        {
            if (msgContent.ToString().Length > 0)
            {
                PushJetMsg.message = msgContent.ToString();
                PushJetRepo.SendMessage(PushJetMsg.Clone());
                Console.WriteLine("PushJet Message Sent");
                PushJetMsg.message = "";
            }
            InitializePushJetMsg();
        }
    }
}
