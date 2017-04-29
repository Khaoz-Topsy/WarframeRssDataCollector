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
            var k = 1;
        }
        public void flush()
        {
            var k = 1;
        }

        public void saveAlerts(List<WarframeAlertItem> Alerts)
        {
            var k = 1;
        }

        public void saveInvasions(List<WarframeNonAlertItem> Invasions)
        {
            var k = 1;
        }

        public void saveOutbreaks(List<WarframeNonAlertItem> Outbreaks)
        {
            var k = 1;
        }
    }
}
