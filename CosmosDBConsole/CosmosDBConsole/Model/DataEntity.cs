using System;
using System.ComponentModel;

namespace CosmosDBConsole.Model
{
    using Microsoft.Azure.Cosmos.Table;
    class DataEntity : TableEntity
    {
        public DataEntity()
        {
        }

        public DataEntity(DateTime now)
        {
            PartitionKey = "Common";
            RowKey = (long.MaxValue - now.ToUniversalTime().Ticks).ToString();
        }



        public string ConnectedDevicesCount { get; set; }
        public string ControllerCount { get; set; }
        public string  RoomStatCount { get; set; }
        public string ItrvsCount { get; set; }
        public string HeimanSmartPlugCount { get; set; }
        public string OwonSmartPlugCount { get; set; }
        public string UnderFloorHeatingCount { get; set; }
        public string HeatingActuatorCount { get; set; }
        public string LoadActuatorCount { get; set; }
        public string WT724R1S0902Count { get; set; }
        public string WT714R1S0902Count { get; set; }
        public string WT704R1S1804Count { get; set; }
        public string WT734R1S0902Count { get; set; }
        public string WT704R1S30S2Count { get; set; }
        public string WT714R1S30S2Count { get; set; }
        public string WT704R1A580HCount { get; set; }
        public string WT714R1A580HCount { get; set; }
        public string WT704R1A30S4Count { get; set; }
        public string WiserHeatBrandCount { get; set; }
        public string AuraConnectBrandCount { get; set; }
        public string EcoModeUsage { get; set; }
        public string ComfortModeUsage { get; set; }
        public string OpenWindowDetectionUsage { get; set; }
        public string ThirtyDayWiserHeatActiveConnections { get; set; }
        public string ThirtyDayAuraConnectActiveConnections { get; set; }


        public string ITRVPopulationCount { get; set; }
        public string HeimanSmartPlugPopulationCount { get; set; }
        public string RoomStatPopulationCount { get; set; }

        public string UnderFloorHeatingPopulationCount { get; set; }
        public string AllDevicesPopulationCount { get; set; }
    }
}
