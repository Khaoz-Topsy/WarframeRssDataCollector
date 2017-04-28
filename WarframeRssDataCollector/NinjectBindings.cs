using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarframeRssDataCollector.Data.DI;
using WarframeRssDataCollector.Data.DI.Interface;
using WarframeRssDataCollector.Data.Repositories;
using WarframeRssDataCollector.Data.Repositories.Interface;

namespace WarframeRssDataCollector
{
    public class NinjectBindings : Ninject.Modules.NinjectModule
    {
        private bool isRemote = true;

        public override void Load()
        {
            //Bind<IBaseRepository>().To<TextFileRepository>();
            Bind<IBaseRepository>().To<DatabaseRepository>();
            //Bind<IBaseRepository>().To<MockRepository>();

            if (isRemote)
            {
                Bind<IConnectionString>().To<RemoteConnectionStrings>();
            }
            else
            {
                Bind<IConnectionString>().To<ConnectionStrings>();
            }
        }
    }
}
