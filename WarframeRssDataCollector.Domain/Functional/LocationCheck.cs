using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarframeRssDataCollector.Data.Functional
{
    public class LocationCheck
    {
        public static bool Check(String Location)
        {
            if (Location.ToLower().Equals("(earth)"))
            {
                return true;
            }
            if (Location.ToLower().Equals("(mercury)"))
            {
                return true;
            }
            if (Location.ToLower().Equals("(mars)"))
            {
                return true;
            }
            if (Location.ToLower().Equals("(venus)"))
            {
                return true;
            }
            if (Location.ToLower().Equals("(saturn)"))
            {
                return true;
            }
            if (Location.ToLower().Equals("(jupiter)"))
            {
                return true;
            }
            if (Location.ToLower().Equals("(phobos)"))
            {
                return true;
            }
            if (Location.ToLower().Equals("(sedna)"))
            {
                return true;
            }
            if (Location.ToLower().Equals("(europa)"))
            {
                return true;
            }
            if (Location.ToLower().Equals("(uranus)"))
            {
                return true;
            }
            if (Location.ToLower().Equals("(neptune)"))
            {
                return true;
            }
            if (Location.ToLower().Equals("(pluto)"))
            {
                return true;
            }
            if (Location.ToLower().Equals("(eris)"))
            {
                return true;
            }
            if (Location.ToLower().Equals("(ceres)"))
            {
                return true;
            }
            if (Location.ToLower().Equals("(void)"))
            {
                return true;
            }
            if (Location.ToLower().Equals("(kuva"))
            {
                return true;
            }

            return false;
        }
    }
}
