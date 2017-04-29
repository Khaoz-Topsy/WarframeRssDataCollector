using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarframeRssDataCollector.Data.Functional;
using WarframeRssDataCollector.Data.Repositories;
using WarframeRssDataCollector.Data.Repositories.Interface;
using WarframeRssDataCollector.Domain.Store.Warframe;

namespace WarframeRssDataCollector
{
    public class AppRunning
    {
        private getRssData rssData = new getRssData();
        private List<WarframeItem> Current = new List<WarframeItem>();
        private List<WarframeItem> Old = new List<WarframeItem>();
        private IBaseRepository storeRepo;
        private int refreshRate = 60000;

        public void start(IBaseRepository ninjectRepo)
        {
            storeRepo = ninjectRepo;

            //Only for the textFileRepo
            storeRepo.deleteFiles();

            while (true)
            {
                refresh();
                System.Threading.Thread.Sleep(refreshRate);
            }
        }

        private void refresh()
        {
            Old = Current;
            Current = rssData.getData();

            if ((Old.Count > 0) && (Current.Count > 0))
            {
                commitDifferences(rssData.compare(Current, Old));
            }
            else
            {
                Console.WriteLine("Warframe Data Collector Initialized");
            }

            //Console.WriteLine("Warframe Data Collector ready for DEV Initialization");
            //var test = Console.ReadLine();
            //commitDifferences(rssData.compare(Current, Old));
        }

        private void commitDifferences(List<WarframeItem> Difference)
        {
            List<WarframeAlertItem> Alerts = new List<WarframeAlertItem>();
            List<WarframeNonAlertItem> Invasions = new List<WarframeNonAlertItem>();
            List<WarframeNonAlertItem> Outbreaks = new List<WarframeNonAlertItem>();

            foreach (WarframeItem item in Difference)
            {
                if (item.Author == "Alert")
                    Alerts.Add((WarframeAlertItem)item);
                if (item.Author == "Invasion")
                    Invasions.Add((WarframeNonAlertItem)item);
                if (item.Author == "Outbreak")
                    Outbreaks.Add((WarframeNonAlertItem)item);
            }

            storeRepo.saveAlerts(Alerts);
            storeRepo.saveInvasions(Invasions);
            storeRepo.saveOutbreaks(Outbreaks);

            storeRepo.flush();
        }

    }
}
