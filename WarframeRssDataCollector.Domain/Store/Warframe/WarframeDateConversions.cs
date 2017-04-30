using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarframeRssDataCollector.Domain.Store.Warframe
{
    public class WarframeDateConversions
    {
        public WarframeDateConversions() { }

        public string ConvertDateString(string Date)
        {
            int start = 0;
            start = Date.IndexOf(" ");
            string Temp = Date.Substring(start);
            string Current = Temp;
            string Old = Temp;


            while (Temp.Contains(":") == true)
            {
                Old = Current;
                Current = Temp;
                Temp = Temp.Substring(start);
            }

            start = Old.Trim().IndexOf(" ");
            return CompareTimes(Old.Trim().Substring(0, start));
        }

        private string CompareTimes(string Time)
        {
            int start = 0;
            start = Time.IndexOf(":");
            int ExpiryHour = Convert.ToInt32(Time.Substring(0, start));
            ExpiryHour += 2;

            if (ExpiryHour > 23)
            {
                ExpiryHour = ExpiryHour - 24;
            }

            string Temp = Time.Substring(start + 1);

            start = Temp.IndexOf(":");
            int ExpiryMinute = Convert.ToInt32(Temp.Substring(0, start));

            int ExpirySecond = Convert.ToInt32(Temp.Substring(start + 1));

            string Now = DateTime.Now.ToString("HH:mm:ss");
            int CurrentTimeHour = Convert.ToInt32(Now.Substring(0, 2)); //Adjusted for Timezone
            int CurrentTimeMinute = Convert.ToInt32(Now.Substring(3, 2));
            int CurrentTimeSecond = Convert.ToInt32(Now.Substring(6, 2));

            int TimeLeftHour;
            int TimeLeftMinute;
            int TimeLeftSecond;

            if (ExpirySecond >= CurrentTimeSecond) //Supposed to be
            {
                TimeLeftSecond = ExpirySecond - CurrentTimeSecond;
            }
            else
            {
                ExpirySecond = ExpirySecond + 60;
                TimeLeftSecond = ExpirySecond - CurrentTimeSecond;
                ExpiryMinute--;
            }

            if (ExpiryMinute >= CurrentTimeMinute) //Supposed to be
            {
                TimeLeftMinute = ExpiryMinute - CurrentTimeMinute;
            }
            else
            {
                ExpiryMinute = ExpiryMinute + 60;
                TimeLeftMinute = ExpiryMinute - CurrentTimeMinute;
                ExpiryHour--;
            }

            if (ExpiryHour >= CurrentTimeHour) //Supposed to be
            {
                TimeLeftHour = ExpiryHour - CurrentTimeHour;
            }
            else
            {
                //ExpiryHour = ExpiryHour + 24;
                //TimeLeftHour = ExpiryHour - CurrentTimeHour;
                return "Expired";
            }

            string Result = "";

            if (TimeLeftHour > 0) { Result = Result + " " + TimeLeftHour + "h"; }
            if (TimeLeftMinute > 0) { Result = Result + " " + TimeLeftMinute + "m"; }
            else { Result = "Expired"; }

            return Result;
        }

        public DateTime getDate(string Date)
        {
            string[] dayStrings = Date.Split(' ');
            int day = int.Parse(dayStrings[1]);
            int month = DateTime.ParseExact(dayStrings[2], "MMM", CultureInfo.CurrentCulture).Month;
            int year = int.Parse(dayStrings[3]);

            string[] timeStrings = getTimeFromDate(Date).Split(':');
            int hour = int.Parse(timeStrings[0]) + 2;
            int min = int.Parse(timeStrings[1]);
            int sec = int.Parse(timeStrings[2]);

            if(hour > 23)
            {
                hour = hour - 24;
                if (day < DateTime.DaysInMonth(year, month))
                {
                    day += 1;
                }
                else
                {
                    month++;
                    day = 1;
                }
            }

            return new DateTime(year, month, day, hour, min, sec);
        }

        private string getTimeFromDate(string Date)
        {
            int start = Date.IndexOf(" ");
            string Temp = Date.Substring(start);
            string Current = Temp;
            string Old = Temp;


            while (Temp.Contains(":") == true)
            {
                Old = Current;
                Current = Temp;
                Temp = Temp.Substring(start);
            }

            start = Old.Trim().IndexOf(" ");
            return Old.Trim().Substring(0, start);
        }
    }
}
