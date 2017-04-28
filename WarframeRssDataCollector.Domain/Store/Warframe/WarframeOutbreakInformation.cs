using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarframeRssDataCollector.Domain.Store.Warframe
{
    public class WarframeOutbreakInformation
    {
        public string Reward { get; set; }
        public string Location { get; set; }
        public bool isPhorid { get; set; }

        public WarframeOutbreakInformation(WarframeNonAlertItem Outbreak)
        {
            try
            {
                String Line = Outbreak.Title;

                string[] rewardLocationSplit = new string[] { " - " };
                string[] rewardLocation = Line.Split(rewardLocationSplit, StringSplitOptions.None);

                Reward = rewardLocation[0];

                if(rewardLocation[1].Contains("PHORID SPAWN"))
                {
                    Location = rewardLocation[1].Replace("PHORID SPAWN ", "");
                    isPhorid = true;
                }
                else
                {
                    Location = rewardLocation[1];
                    isPhorid = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Outbreak information failed to be decoded");
            }
        }

        public bool isValid()
        {
            if ((Reward.ToLower().Contains("N/A"))      ||      (Reward == null))       { return false; }
            if ((Location.ToLower().Contains("N/A"))    ||      (Location == null))     { return false; }

            return true;
        }
    }
}
