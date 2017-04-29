using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarframeRssDataCollector.Domain.Enums;

namespace WarframeRssDataCollector.Domain.Functional
{
    public class WantedList
    {
        public static bool Check(String Title)
        {
            WantedListEnum[] list = (WantedListEnum[])Enum.GetValues(typeof(WantedListEnum));

            foreach (WantedListEnum item in list)
            {
                if(Title.ToLower().Contains(item.ToString()))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
