using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WarframeRssDataCollector.Data.Functional;
using WarframeRssDataCollector.Data.Repositories.Interface;
using WarframeRssDataCollector.Domain.Store.Warframe;

namespace WarframeRssDataCollector
{
    class Program
    {
        static void Main(string[] args)
        {
            StandardKernel _Kernel = new StandardKernel();
            _Kernel.Load(Assembly.GetExecutingAssembly());
            IBaseRepository baseRepo = _Kernel.Get<IBaseRepository>();

            AppRunning app = new AppRunning();
            app.start(baseRepo);
        }
    }
}
