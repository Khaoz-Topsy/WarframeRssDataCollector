using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WarframeRssDataCollector.Data.DI.Interface;
using WarframeRssDataCollector.Data.Functional;
using WarframeRssDataCollector.Data.Repositories.Interface;
using WarframeRssDataCollector.Domain.Store.Warframe;

namespace WarframeRssDataCollector.Data.Repositories
{
    public class DatabaseRepository : IBaseRepository
    {
        private IConnectionString connStr;

        public DatabaseRepository(IConnectionString _connStr)
        {
            connStr = _connStr;
        }

        public async void saveAlerts(List<WarframeAlertItem> Alerts)
        {
			
        }

        public async void saveInvasions(List<WarframeNonAlertItem> Invasions)
        {
			
        }

        public async void saveOutbreaks(List<WarframeNonAlertItem> Outbreaks)
        {
			
        }

        public void deleteFiles()
        {

        }
    }
}
