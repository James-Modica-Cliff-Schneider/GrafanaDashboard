using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace CosmosDBConsole
{
    using System.Threading.Tasks;
    using Microsoft.Azure.Cosmos.Table;
    using Model;
    class DataSamples
    {
        public async Task RunSamples()
        {
            

            string tableName = "DataTimeSeries";

            // Create or reference an existing table
            CloudTable table = await Common.CreateTableAsync(tableName);

            // Insert data functionality 
            await DataSamplesInsertAsync(table);
        }

        private async Task DataSamplesInsertAsync(CloudTable table)
        {
            DataRetrivalModel t = new DataRetrivalModel();
            
            List<dynamic> ConnectedDevicesCountList = await t.ReadConnectedDevicesCount();
            List<dynamic> ControllerCountList = await t.ReadDeviceCount("Controller");
            List<dynamic> ITRVsCountList = await t.ReadDeviceCount("iTRV");
            List<dynamic> RoomStatCountList = await t.ReadDeviceCount("RoomStat");
            List<dynamic> HeimanPlugCountList = await t.ReadDeviceCount("HeimanSmartplug");
            List<dynamic> OwonPlugCountList = await t.ReadDeviceCount("OwonSmartplug");
            List<dynamic> UnderFloorCountList = await t.ReadDeviceCount("UnderFloorHeating");
            List<dynamic> HeatingActuatorCountList = await t.ReadDeviceCount("HeatingActuator");
            List<dynamic> LoadActuatorCountList = await t.ReadDeviceCount("LoadActuator");
            List<dynamic> WT724R1S0902CountList = await t.ReadHubTypeCount("WT724R1S0902");
            List<dynamic> WT714R1S0902CountList = await t.ReadHubTypeCount("WT714R1S0902");
            List<dynamic> WT704R1S1804CountList = await t.ReadHubTypeCount("WT704R1S1804");
            List<dynamic> WT734R1S0902CountList = await t.ReadHubTypeCount("WT734R1S0902");
            List<dynamic> WT704R1S30S2CountList = await t.ReadHubTypeCount("WT704R1S30S2");
            List<dynamic> WT714R1S30S2CountList = await t.ReadHubTypeCount("WT714R1S30S2");
            List<dynamic> WT704R1A580HCountList = await t.ReadHubTypeCount("WT704R1A580H");
            List<dynamic> WT714R1A580HCountList = await t.ReadHubTypeCount("WT714R1A580H");
            List<dynamic> WT704R1A30S4CountList = await t.ReadHubTypeCount("WT704R1A30S4");
            List<int> WiserHeatBrandCountList = await t.ReadWiserAuraCount("WiserHeat");
            List<int> AuraConnectBrandCountList = await t.ReadWiserAuraCount("AuraConnect");
            List<dynamic> EcoModeUsageList = await t.ReadFeatureUsage("EcoModeEnabled");
            List<dynamic> ComfortModeUsageList = await t.ReadFeatureUsage("ComfortModeEnabled");
            int OpenWindowDetectionUsageValue = await t.ReadOpenWindowDetectionUsage();
            List<int> ThirtyDayWiserHeatActiveConnectionsList = await t.ReadThirtyDayActiveConnections("WiserHeat");

            List<int> ThirtyDayAuraConnectActiveConnectionsList = await t.ReadThirtyDayActiveConnections("AuraConnect");

            List<int> ITRVPopulationCountList = await t.ReadPopulationCount("iTRV");
            List<int> HeimanPlugPopulationCountList = await t.ReadPopulationCount("HeimanSmartplug");
            List<int> RoomStatPopulationCountList = await t.ReadPopulationCount("RoomStat");
            List<int> UnderFloorHeatingPopulationCountList = await t.ReadPopulationCount("UnderFloorHeating");
            List<int> AllDevicesPopulationCountList = await t.ReadAllPopulationCount();


            // Create an instance of a data entity. See the Model\DataEntity.cs for a description of the entity.
            DataEntity data = new DataEntity(DateTime.Now)
            {
                ConnectedDevicesCount = Convert.ToString(ConnectedDevicesCountList[0]),
                ControllerCount = Convert.ToString(ControllerCountList[0]),
                RoomStatCount = Convert.ToString(RoomStatCountList[0]),
                ItrvsCount = Convert.ToString(ITRVsCountList[0]),
                HeimanSmartPlugCount = Convert.ToString(HeimanPlugCountList[0]),
                OwonSmartPlugCount = Convert.ToString(OwonPlugCountList[0]),
                UnderFloorHeatingCount = Convert.ToString(UnderFloorCountList[0]),
                HeatingActuatorCount = Convert.ToString(HeatingActuatorCountList[0]),
                LoadActuatorCount = Convert.ToString(LoadActuatorCountList[0]),
                WT724R1S0902Count = Convert.ToString(WT724R1S0902CountList[0]),
                WT714R1S0902Count = Convert.ToString(WT714R1S0902CountList[0]),
                WT704R1S1804Count = Convert.ToString(WT704R1S1804CountList[0]),
                WT734R1S0902Count = Convert.ToString(WT734R1S0902CountList[0]),
                WT704R1S30S2Count = Convert.ToString(WT704R1S30S2CountList[0]),
                WT714R1S30S2Count = Convert.ToString(WT714R1S30S2CountList[0]),
                WT704R1A580HCount = Convert.ToString(WT704R1A580HCountList[0]),
                WT714R1A580HCount = Convert.ToString(WT714R1A580HCountList[0]),
                WT704R1A30S4Count = Convert.ToString(WT704R1A30S4CountList[0]),
                WiserHeatBrandCount = JsonConvert.SerializeObject(WiserHeatBrandCountList),
                AuraConnectBrandCount = JsonConvert.SerializeObject(AuraConnectBrandCountList),
                EcoModeUsage = Convert.ToString(EcoModeUsageList[0]),
                ComfortModeUsage = Convert.ToString(ComfortModeUsageList[0]),
                OpenWindowDetectionUsage = Convert.ToString(OpenWindowDetectionUsageValue),

                ThirtyDayWiserHeatActiveConnections = JsonConvert.SerializeObject(ThirtyDayWiserHeatActiveConnectionsList),
                ThirtyDayAuraConnectActiveConnections = JsonConvert.SerializeObject(ThirtyDayAuraConnectActiveConnectionsList),

                ITRVPopulationCount = JsonConvert.SerializeObject(ITRVPopulationCountList),
                HeimanSmartPlugPopulationCount = JsonConvert.SerializeObject(HeimanPlugPopulationCountList),
                RoomStatPopulationCount = JsonConvert.SerializeObject(RoomStatPopulationCountList),
                UnderFloorHeatingPopulationCount = JsonConvert.SerializeObject(UnderFloorHeatingPopulationCountList),
                AllDevicesPopulationCount = JsonConvert.SerializeObject(AllDevicesPopulationCountList)
            };

            // Insert the entity
            data = await CRUDUtils.InsertOrMergeEntityAsync(table, data);
            
        }

        
    }
}
