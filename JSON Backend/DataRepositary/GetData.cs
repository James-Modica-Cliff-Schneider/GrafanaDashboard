using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataRepository.DataModel;
using Microsoft.Azure.Cosmos.Table;
using Model;

namespace DataRepository
{
    
    public class GetData : ITimeSeriesData

    {
        private CloudTable _table;
        
        private List<DeviceCounts> _dataTimeSeriesTableList = new List<DeviceCounts>();
        private List<EcoIQTimingCount> _ecoIQTimingTableList = new List<EcoIQTimingCount>();
        private List<MostCommonProblem> _mostCommonRecentProblemTableList = new List<MostCommonProblem>();
        private List<DeviecLocationAndStatusCount> _deviceLocationAndStatusTableList = new List<DeviecLocationAndStatusCount>();


        public async Task<List<DeviceCounts>> GetDataTimeSeriesData(DateTime to, DateTime from, string tablename)
        {
            await QueryDataTimeSeriesTable(to, from, tablename);

            return _dataTimeSeriesTableList;
        }

        public async Task<List<DeviceCounts>> GetDataSeriesData(DateTime to, DateTime from, string tablename)
        {
            await QueryDataTimeSeriesTable(to, from, tablename);

            return _dataTimeSeriesTableList;
        }

        public async Task<List<MostCommonProblem>> GetMostCommonRecentProblemsData(string tablename)
        {
            await QueryMostCommonRecentProblemsTable(tablename);

            return _mostCommonRecentProblemTableList;
        }

        public async Task<List<DeviecLocationAndStatusCount>> GetDeviceLocationAndStatusData(DateTime to, DateTime from, string tablename)
        {
            await QueryDeviceLocationsAndStatusTable(to, from, tablename);

            return _deviceLocationAndStatusTableList;
        }
        public async Task<List<EcoIQTimingCount>> GetDataEcoIQTimeSeriesData(DateTime to, DateTime from, string tablename)
        {
            await QueryEcoIQTimingTable(to, from, tablename);

            return _ecoIQTimingTableList;
        }

        public async Task ConnectToTable(string tablename)
        {
            _table = await Common.ConnectTableAsync(tablename);
        }

        public async Task QueryDataTimeSeriesTable(DateTime to, DateTime from,string tablename)
        { 

            await ConnectToTable(tablename);

            var entityList = await Utils.DataTimeSeriesPartitionRangeQueryAsync(_table, "Common", to, from);
                _dataTimeSeriesTableList = entityList
                    .OrderByDescending(entity => entity.RowKey)
                    .Select(entity => new DeviceCounts(entity.RowDateTime(), entity.ConnectedDevicesCount, entity.ControllerCount, entity.HeatingActuatorCount, entity.HeimanSmartPlugCount, entity.ItrvsCount, entity.LoadActuatorCount, entity.OwonSmartPlugCount, entity.RoomStatCount, entity.UnderFloorHeatingCount, entity.WT724R1S0902Count, entity.WT714R1S0902Count, entity.WT704R1S1804Count, entity.WT734R1S0902Count, entity.WT704R1S30S2Count, entity.WT714R1S30S2Count, entity.WT704R1A580HCount, entity.WT714R1A580HCount, entity.WT704R1A30S4Count, entity.ITRVPopulationCount, entity.HeimanSmartPlugPopulationCount, entity.RoomStatPopulationCount, entity.UnderFloorHeatingPopulationCount,entity.AllDevicesPopulationCount, entity.WiserHeatBrandCount, entity.AuraConnectBrandCount, entity.EcoModeUsage, entity.ComfortModeUsage, entity.ThirtyDayWiserHeatActiveConnections,entity.ThirtyDayAuraConnectActiveConnections, entity.OpenWindowDetectionUsage))
                    .ToList();
        }

        public async Task QueryEcoIQTimingTable(DateTime to, DateTime from, string tablename)
        {

            await ConnectToTable(tablename);

            var entityList = await Utils.EcoIQTimingPartitionRangeQueryAsync(_table, "EcoIQ", to, from);
                _ecoIQTimingTableList = entityList
                    .Select(entity => new EcoIQTimingCount(entity.RowDateTime(),entity.EcoModeEnabledMessagesProcessedInLastMinute, entity.ScheduleUpdateMessagesProcessedInLastMinute, entity.TelemetryMessageProcessedInLastMinute))
                    .ToList();
        }

        public async Task QueryMostCommonRecentProblemsTable(string tablename)
        {

            await ConnectToTable(tablename);

            var entityList = await Utils.MostCommonRecentproblemPartitionRangeQueryAsync(_table, "EcoIQ");
                _mostCommonRecentProblemTableList = entityList
                    .Select(entity => new MostCommonProblem(entity.RowKey ,entity.Count))
                        .ToList();
        }

        public async Task QueryDeviceLocationsAndStatusTable(DateTime to, DateTime from, string tablename)
        {

            await ConnectToTable(tablename);

            var entityList = await Utils.DeviceLocationAndStatusPartitionRangeQueryAsync(_table, "EcoIQ", to, from);
                _deviceLocationAndStatusTableList = entityList
                    .Select(entity => new DeviecLocationAndStatusCount(entity.RowKey, entity.TimeStamp, entity.HasError, entity.IsInEcoModePeriod, entity.EcoModeEnabled, entity.Longitude, entity.Latitude))
                        .ToList();
        }
    }
}
