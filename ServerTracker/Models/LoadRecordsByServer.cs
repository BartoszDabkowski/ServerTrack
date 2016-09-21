using ServerTracker.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ServerTracker.Models
{
    public class LoadRecordsByServer
    {
        private Dictionary<string, List<LoadRecord>> LoadRecordsByServerDictionary { get; }

        public LoadRecordsByServer()
        {
            LoadRecordsByServerDictionary = new Dictionary<string, List<LoadRecord>>();
            
        }

        public void AddServer(string serverName)
        {
            LoadRecordsByServerDictionary.Add(serverName, new List<LoadRecord>());
        }

        public bool ContainsServer(string serverName)
        {
            return LoadRecordsByServerDictionary.ContainsKey(serverName);
        }

        public void AddLoadForServer(string serverName, LoadRecord loadRecord)
        {
            if (!LoadRecordsByServerDictionary.ContainsKey(serverName))
                throw new KeyNotFoundException();

            LoadRecordsByServerDictionary[serverName].Add(loadRecord);
        }

        public RamLoadAveragesDto GetRamLoadAverages(string serverName)
        {
            return new RamLoadAveragesDto()
            {
                AverageRamLoadsForLastHourByMinute = 
                    GetAverageRamLoadsForLastRateByMinute(serverName, Global.Utility.MinutesInHour, Global.Utility.MinutesInMinute),

                AverageRamLoadsForLastDayByHour = 
                    GetAverageRamLoadsForLastRateByMinute(serverName, Global.Utility.HoursInDay, Global.Utility.MinutesInHour)
            };
        }

        private double[] GetAverageRamLoadsForLastRateByMinute(string serverName, int rate, int minutesInRate)
        {
            if(!LoadRecordsByServerDictionary.ContainsKey(serverName))
                throw new KeyNotFoundException();

            var averageRamLoadsForLastRateByMinute = new List<double>();
            var loadRecords = LoadRecordsByServerDictionary[serverName];
            var dateTimeNow = DateTime.Now;

            var index = loadRecords.Count - 1;
            for (var minutes = 1; minutes <= rate && index >= 0; minutes++)
            {
                var sumOfRamLoads = 0.0;
                var totalRamLoads = 0;
                while (index >= 0 && dateTimeNow.AddMinutes(-minutes * minutesInRate) <= loadRecords[index].DateTime)
                {
                    Debug.WriteLine(loadRecords[index--].RamLoad);
                    sumOfRamLoads += loadRecords[index--].RamLoad;
                    totalRamLoads++;
                }
                var averageRamLoads = totalRamLoads != 0 ? sumOfRamLoads/totalRamLoads : 0.0;

                Debug.WriteLine(sumOfRamLoads + " " + totalRamLoads);
                averageRamLoadsForLastRateByMinute.Add(averageRamLoads);
            }

            return averageRamLoadsForLastRateByMinute.ToArray();
        }

        public void InitializeServersLoadRecordsRandom(string serverName, double minimum, double maximim)
        {
            if (!LoadRecordsByServerDictionary.ContainsKey(serverName))
                throw new KeyNotFoundException();

            var dateTimeNow = DateTime.Now;

            for (var seconds = Global.Utility.SecondsInDay; seconds >= 0; seconds--)
            {
                LoadRecordsByServerDictionary[serverName].Add(
                    new LoadRecord
                    {
                        DateTime = Global.Utility.CreateIncrementedDateTime(dateTimeNow, seconds),
                        CpuLoad = Global.Utility.CreateRandomLoad(minimum, maximim),
                        RamLoad = Global.Utility.CreateRandomLoad(minimum, maximim)
                    });
            }
        }

        public void InitializeServersLoadRecords(string serverName, double load)
        {
            if (!LoadRecordsByServerDictionary.ContainsKey(serverName))
                throw new KeyNotFoundException();

            var dateTimeNow = DateTime.Now;

            for (var seconds = Global.Utility.SecondsInDay; seconds >= 0; seconds--)
            {
                LoadRecordsByServerDictionary[serverName].Add(
                    new LoadRecord
                    {
                        DateTime = Global.Utility.CreateIncrementedDateTime(dateTimeNow, seconds),
                        CpuLoad = load,
                        RamLoad = load
                    });
            }
        }
    }
}