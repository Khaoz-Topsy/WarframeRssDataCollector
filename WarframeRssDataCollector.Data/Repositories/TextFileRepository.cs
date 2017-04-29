using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarframeRssDataCollector.Domain.Store.Warframe;

namespace WarframeRssDataCollector.Data.Repositories.Interface
{
    public class TextFileRepository : IBaseRepository
    {
        string alertFile = "..\\Alerts.txt";
        string invasionFile = "..\\Invasions.txt";
        string outbreakFile = "..\\Outbreaks.txt";

        public void saveAlerts(List<WarframeAlertItem> Alerts)
        {
            foreach (WarframeAlertItem alert in Alerts)
            {
                WarframeAlertInformation info = new WarframeAlertInformation(alert);
                System.IO.StreamWriter file = File.AppendText(alertFile);
                file.WriteLine(alert.ToString());
                file.Close();
            }
        }

        public void saveInvasions(List<WarframeNonAlertItem> Invasions)
        {
            foreach (WarframeNonAlertItem invasion in Invasions)
            {
                WarframeInvasionInformation info = new WarframeInvasionInformation(invasion);
                System.IO.StreamWriter file = File.AppendText(invasionFile);
                file.WriteLine(invasion.ToString());
                file.Close();
            }
        }

        public void saveOutbreaks(List<WarframeNonAlertItem> Outbreaks)
        {
            foreach (WarframeNonAlertItem outbreak in Outbreaks)
            {
                WarframeOutbreakInformation info = new WarframeOutbreakInformation(outbreak);
                System.IO.StreamWriter file = File.AppendText(outbreakFile);
                file.WriteLine(outbreak.ToString());
                file.Close();
            }
        }


        public void deleteFiles()
        {
            if (File.Exists(alertFile))
                File.Delete(alertFile);

            if (File.Exists(invasionFile))
                File.Delete(invasionFile);

            if (File.Exists(outbreakFile))
                File.Delete(outbreakFile);
        }
        public void flush() { }
    }
}
