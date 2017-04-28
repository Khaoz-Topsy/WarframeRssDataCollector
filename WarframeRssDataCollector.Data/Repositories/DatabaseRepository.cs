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
            foreach (WarframeAlertItem alert in Alerts)
            {
                int retries = 0;
                bool passed = false;

                do
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connStr.getString()))
                        {
                            WarframeAlertInformation info = new WarframeAlertInformation(alert);

                            if (alert.isValid() && info.isValid())
                            {
                                await conn.OpenAsync();
                                string query = "INSERT INTO " + SecretData.TableAlertData +
                                            "(" +
                                                "GUID, Title, Author, Descrip, Money, " +
                                                "Reward, RewardType, Location, Faction, PubDate, Expiry" +
                                            ") " +
                                        "VALUES " +
                                            "(" +
                                                "@guid, @title, @author, @desc, @money, " +
                                                "@reward, @rewardtype, @location, @faction, @pubdate, @expiry" +
                                            "); ";
                                SqlCommand command = new SqlCommand(query, conn);
                                command.Parameters.AddWithValue("@guid", alert.Guid);
                                command.Parameters.AddWithValue("@title", alert.Title);
                                command.Parameters.AddWithValue("@author", alert.Author);
                                command.Parameters.AddWithValue("@desc", alert.Description);
                                command.Parameters.AddWithValue("@money", info.Money);

                                command.Parameters.AddWithValue("@reward", info.Reward);
                                command.Parameters.AddWithValue("@rewardtype", info.RewardType);
                                command.Parameters.AddWithValue("@location", info.Location);
                                command.Parameters.AddWithValue("@faction", alert.Faction);
                                command.Parameters.AddWithValue("@pubdate", alert.PubDate);
                                command.Parameters.AddWithValue("@expiry", alert.Expiry);

                                await command.ExecuteNonQueryAsync();

                                passed = true;
                                Console.WriteLine("DB Entry made: " + alert.Title);
                            }
                            else
                            {
                                Console.WriteLine(
                                    "Alert is Valid: " + alert.isValid() +
                                    "Info is Valid: " + info.isValid()
                                    );
                            }
                        }
                    }
                    catch (Exception)
                    {
                        retries++;
                        if (retries == 1) { Console.WriteLine("DB Entry failed for: " + alert.Title); }
                        else { Console.WriteLine("\t Attempt #" + retries); }
                    }
                }
                while ((retries < 3) && (passed == false));
            }
        }

        public async void saveInvasions(List<WarframeNonAlertItem> Invasions)
        {
            foreach (WarframeNonAlertItem invasion in Invasions)
            {
                int retries = 0;
                bool passed = false;

                do
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connStr.getString()))
                        {
                            WarframeInvasionInformation info = new WarframeInvasionInformation(invasion);

                            if (invasion.isValid() && info.isValid())
                            {
                                await conn.OpenAsync();
                                string query = "INSERT INTO " + SecretData.TableInvasionData +
                                            "(" +
                                                "GUID, Title, Author, PubDate, Faction1, " +
                                                "Faction1Reward, Faction2, Faction2Reward, Location" +
                                            ") " +
                                        "VALUES " +
                                            "(" +
                                                "@guid, @title, @author, @pubdate, @fac1, " +
                                                "@fac1reward, @fac2, @fac2reward, @location" +
                                            "); ";
                                SqlCommand command = new SqlCommand(query, conn);
                                command.Parameters.AddWithValue("@guid", invasion.Guid);
                                command.Parameters.AddWithValue("@title", invasion.Title);
                                command.Parameters.AddWithValue("@author", invasion.Author);
                                command.Parameters.AddWithValue("@pubdate", invasion.PubDate);
                                command.Parameters.AddWithValue("@fac1", info.faction1);
                                command.Parameters.AddWithValue("@fac2", info.faction2);
                                command.Parameters.AddWithValue("@fac1reward", info.faction1Reward);
                                command.Parameters.AddWithValue("@fac2reward", info.faction2Reward);
                                command.Parameters.AddWithValue("@location", info.location);

                                await command.ExecuteNonQueryAsync();

                                passed = true;
                                Console.WriteLine("DB Entry made: " + invasion.Title);
                            }
                            else
                            {
                                Console.WriteLine(
                                    "Invasion is Valid: " + invasion.isValid() +
                                    "Info is Valid: " + info.isValid()
                                    );
                            }
                        }
                    }
                    catch (Exception)
                    {
                        retries++;
                        if (retries == 1) { Console.WriteLine("DB Entry failed for: " + invasion.Title); }
                        else { Console.WriteLine("\t Attempt #" + retries); }
                    }
                }
                while ((retries < 3) && (passed == false));
            }
        }

        public async void saveOutbreaks(List<WarframeNonAlertItem> Outbreaks)
        {
            foreach (WarframeNonAlertItem outbreak in Outbreaks)
            {
                int retries = 0;
                bool passed = false;

                do
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connStr.getString()))
                        {
                            WarframeOutbreakInformation info = new WarframeOutbreakInformation(outbreak);

                            if (outbreak.isValid() && info.isValid())
                            {
                                await conn.OpenAsync();
                                string query = "INSERT INTO " + SecretData.TableOutbreakData +
                                            "(" +
                                                "GUID, Title, Author, PubDate, " +
                                                "Reward, Location, isPhorid" +
                                            ") " +
                                        "VALUES " +
                                            "(" +
                                                "@guid, @title, @author, @pubdate, " +
                                                "@reward, @location, @isphorid" +
                                            "); ";
                                SqlCommand command = new SqlCommand(query, conn);
                                command.Parameters.AddWithValue("@guid", outbreak.Guid);
                                command.Parameters.AddWithValue("@title", outbreak.Title);
                                command.Parameters.AddWithValue("@author", outbreak.Author);
                                command.Parameters.AddWithValue("@pubdate", outbreak.PubDate);
                                command.Parameters.AddWithValue("@reward", info.Reward);
                                command.Parameters.AddWithValue("@location", info.Location);
                                command.Parameters.AddWithValue("@isphorid", info.isPhorid);

                                await command.ExecuteNonQueryAsync();

                                passed = true;
                                Console.WriteLine("DB Entry made: " + outbreak.Title);
                            }
                            else
                            {
                                Console.WriteLine(
                                    "Outbreak is Valid: " + outbreak.isValid() +
                                    "Info is Valid: " + info.isValid()
                                    );
                            }
                        }
                    }
                    catch (Exception)
                    {
                        retries++;
                        if (retries == 1) { Console.WriteLine("DB Entry failed for: " + outbreak.Title); }
                        else { Console.WriteLine("\t Attempt #" + retries); }
                    }
                }
                while ((retries < 3) && (passed == false));
            }
        }

        public void deleteFiles()
        {

        }
    }
}
