using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
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

        private static System.Timers.Timer myTimer;
        static ManualResetEvent _quitEvent = new ManualResetEvent(false);

        public void start(IBaseRepository ninjectRepo)
        {
            storeRepo = ninjectRepo;

            //Only for the textFileRepo
            storeRepo.deleteFiles();

            refresh();
                                            //min sec milli
            myTimer = new System.Timers.Timer(1 * 60 * 1000);
            myTimer.Elapsed += timerTick;
            myTimer.Start();

            //https://stackoverflow.com/questions/2586612/how-to-keep-a-net-console-app-running?noredirect=1&lq=1
            Console.CancelKeyPress += myHandler;
            _quitEvent.WaitOne();
        }

        void myHandler(object sender, ConsoleCancelEventArgs args)
        {
            myTimer.Stop();
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
                Console.Clear();
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

        private void timerTick(object src, ElapsedEventArgs e)
        {
            refresh();
        }
    }
}
