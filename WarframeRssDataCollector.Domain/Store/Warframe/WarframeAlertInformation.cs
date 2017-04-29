using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarframeRssDataCollector.Data.Functional;

namespace WarframeRssDataCollector.Domain.Store.Warframe
{
    public class WarframeAlertInformation
    {
        public String Money = "N/A";
        public String MoneyLong = "N/A";
        public String Reward = "N/A";
        public String RewardType = "N/A";
        public String Location = "N/A";
        public String Description = "N/A";
        public String Time = "N/A";
        public String Versus = "N/A";

        public WarframeAlertInformation(WarframeAlertItem Alert)
        {
            try
            {
                String Line = Alert.Title;

                int count = 0;
                string[] ST = Line.Split(' ');
                String Current = ST[count];
                String Old = "";
                bool EOL = false;
                do
                {
                    if (Current.Contains("cr"))
                    {
                        String TempMoney = Current.Substring(0, Current.Length - 2);
                        if (isNumeric(TempMoney))
                        {
                            Money = TempMoney;
                            MoneyLong = Current;
                        }
                    }

                    if (Current.Contains("m"))
                    {
                        if (isNumeric(Current.Substring(0, Current.Length - 1)))
                        {
                            Time = Current;
                        }
                    }

                    if (Current.Contains("(Mod)"))
                    {
                        Reward = Line.Split('-')[0];
                        RewardType = "Mod";
                    }
                    if (Current.Contains("(Key)"))
                    {
                        Reward = Line.Split('-')[0];
                        RewardType = "Key";
                    }
                    if (Current.Contains("(Aura)"))
                    {
                        Reward = Line.Split('-')[0];
                        RewardType = "Aura";
                    }
                    if (Current.Contains("(Item)"))
                    {
                        Reward = Line.Split('-')[0];
                        RewardType = "Item";
                    }
                    if (Current.Contains("(Blueprint)"))
                    {
                        Reward = Line.Split('-')[0];
                        if (!RewardType.Equals("Vauban"))
                        {
                            RewardType = "Blueprint";
                        }
                    }
                    if (Current.Contains("Vauban"))
                    {
                        Reward = Line.Split('-')[0];
                        RewardType = "Vauban";
                    }
                    if (Current.Contains("(Resource)"))
                    {
                        Reward = Line.Split('-')[0];
                        RewardType = "Resource";
                    }
                    if (Current.Contains("ENDO"))
                    {
                        Reward = Line.Split('-')[0];
                        RewardType = "ENDO";
                    }

                    if (LocationCheck.Check(Current))
                    {
                        Location = Old + " " + Current;
                    }

                    if (RewardType.Contains("N/A"))
                    {
                        Reward = MoneyLong;
                        RewardType = "Money";
                    }

                    if (count < (ST.Length-1))
                    {
                        count++;

                        if (Old.ToLower().Equals("(kuva"))
                            Location += " " + Current;

                        Old = Current;
                        Current = ST[count];
                    }
                    else
                    {
                        EOL = true;
                    }

                } while (!EOL);

                Description = Alert.Description;

                if (Money.Equals("N/A"))
                {
                    Reward = Line.Split('-')[0];
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Alert information failed to be decoded");
            }
        }
        
        public bool isValid()
        {
            if ((Money.ToLower().Contains("N/A"))           ||      (Money == null)) { return false; }
            if ((MoneyLong.ToLower().Contains("N/A"))       ||      (MoneyLong == null)) { return false; }
            if ((Reward.ToLower().Contains("N/A"))          ||      (Reward == null)) { return false; }
            if ((RewardType.ToLower().Contains("N/A"))      ||      (RewardType == null)) { return false; }
            if ((Location.ToLower().Contains("N/A"))        ||      (Location == null)) { return false; }
            if ((Description.ToLower().Contains("N/A"))     ||      (Description == null)) { return false; }
            if ((Time.ToLower().Contains("N/A"))            ||      (Time == null)) { return false; }
            if ((Versus.ToLower().Contains("N/A"))          ||      (Versus == null)) { return false; }

            return true;
        }
        
        private static bool isNumeric(String Num)
        {
            for (int a = 0; a < Num.Length; a++)
            {
                if (!char.IsDigit(Num.ElementAt(a)))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
