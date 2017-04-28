using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarframeRssDataCollector.Domain.Store.Warframe;

namespace WarframeRssDataCollector.Data.Repositories.Interface
{
    public interface IBaseRepository
    {
        void saveAlerts(List<WarframeAlertItem> Alerts);
        void saveInvasions(List<WarframeNonAlertItem> Invasions);
        void saveOutbreaks(List<WarframeNonAlertItem> Outbreaks);

        void deleteFiles();
    }
}
