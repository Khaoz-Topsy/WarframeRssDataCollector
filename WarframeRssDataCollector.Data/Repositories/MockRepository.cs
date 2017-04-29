using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarframeRssDataCollector.Data.Repositories.Interface;
using WarframeRssDataCollector.Domain.Store.Warframe;

namespace WarframeRssDataCollector.Data.Repositories
{
    public class MockRepository : IBaseRepository
    {
        public void deleteFiles()
        {

        }
        public void flush()
        {

        }

        public void saveAlerts(List<WarframeAlertItem> Alerts)
        {

        }

        public void saveInvasions(List<WarframeNonAlertItem> Invasions)
        {

        }

        public void saveOutbreaks(List<WarframeNonAlertItem> Outbreaks)
        {

        }
    }
}
