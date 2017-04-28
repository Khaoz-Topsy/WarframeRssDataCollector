using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarframeRssDataCollector.Domain.Store.Warframe
{
    public class WarframeInvasionInformation
    {
        public string faction1 = "N/A";
        public string faction1Reward = "N/A";
        public string faction2 = "N/A";
        public string faction2Reward = "N/A";
        public string location = "N/A";

        public WarframeInvasionInformation(WarframeNonAlertItem Invasion)
        {
            try
            {
                String Line = Invasion.Title;
            
                string[] rewardLocationSplit = new string[] { " - " };
                string[] rewardLocation = Line.Split(rewardLocationSplit, StringSplitOptions.None);
                location = rewardLocation[rewardLocation.Length-1];

                string[] rewardSplit = new string[] { "VS. " };
                string[] rewards = rewardLocation[0].Split(rewardSplit, StringSplitOptions.None);
                
                String leftTeam = rewards[0];
                String rightTeam = rewards[1];

                string[] leftValues = leftTeam.Split(' ');
                string[] rightValues = rightTeam.Split(' ');

                faction1 = leftValues[0];
                faction1Reward = leftTeam.Replace(leftValues[0], "").Replace("(", "").Replace(")", "").Trim();
                faction2 = rightValues[0];
                faction2Reward = rightTeam.Replace(rightValues[0], "").Replace("(", "").Replace(")", "").Trim();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invasion information failed to be decoded");
            }
        }

        public bool isValid()
        {
            if ((faction1.ToLower().Contains("N/A"))        ||      (faction1 == null))         { return false; }
            if ((faction1Reward.ToLower().Contains("N/A"))  ||      (faction1Reward == null))   { return false; }
            if ((faction2.ToLower().Contains("N/A"))        ||      (faction2 == null))         { return false; }
            if ((faction2Reward.ToLower().Contains("N/A"))  ||      (faction2Reward == null))   { return false; }
            if ((location.ToLower().Contains("N/A"))        ||      (location == null))         { return false; }

            return true;
        }
    }
}
