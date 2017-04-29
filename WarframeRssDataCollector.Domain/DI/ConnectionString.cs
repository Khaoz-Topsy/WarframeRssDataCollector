using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarframeRssDataCollector.Data.DI.Interface;
using WarframeRssDataCollector.Data.Functional;

namespace WarframeRssDataCollector.Data.DI
{
    public class RemoteConnectionStrings : IConnectionString
    {
        public string getString()
        {
            return SecretData.RelConnctionString;
        }
    }
    public class ConnectionStrings : IConnectionString
    {
        public string getString()
        {
            return SecretData.DevConnctionString; 
        }
    }
}
