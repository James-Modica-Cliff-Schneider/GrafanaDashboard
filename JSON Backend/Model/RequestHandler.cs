using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DbNetLink.DbNetSuite;
using Model.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace Model
{
    public class RequestHandler
    {
        private ITimeSeriesData _newTimeSeriesData;

        public RequestHandler(ITimeSeriesData timeSeriesData)
        {
            _newTimeSeriesData = timeSeriesData;
        }

        public async Task<List<DataSeries<int>>> GetOutputTimeSeries(DateTime dataTo, DateTime dataFrom, TargetViewModel[] queryToPreform, string tablename)
        {

            var outputSeries = new List<DataSeries<int>>();

            var entityList = await _newTimeSeriesData.GetDataTimeSeriesData(dataTo, dataFrom, tablename);

            

            foreach (TargetViewModel queryName in queryToPreform)
            {
                List<DataPoint<int>> outputPointsList = new List<DataPoint<int>>();

                

                foreach (DeviceCounts device in entityList)
                {

                        int value = (int)typeof(DeviceCounts).GetProperty(queryName.Target).GetValue(device);

                        DataPoint<int> outputPoint = new DataPoint<int>(device.Date, value);

                        outputPointsList.Add(outputPoint);
                    
                    
                }

                DataPoint<int>[] outputPointArray = outputPointsList.ToArray();

                DataSeries<int> currentOutputSeries = new DataSeries<int>(queryName.Target, outputPointArray);

                outputSeries.Add(currentOutputSeries);
            }

            return outputSeries;
        }

        public async Task<List<DataSeries<int>>> GetOutputSeries(DateTime dataTo, DateTime dataFrom, TargetViewModel[] queryToPreform, string tablename)
        {

            var outputSeries = new List<DataSeries<int>>();

            var entityList = await _newTimeSeriesData.GetDataSeriesData(dataTo, dataFrom, tablename);


            foreach (TargetViewModel queryName in queryToPreform)
            {
                foreach (DeviceCounts device in entityList)
                {
                    var value = (string)typeof(DeviceCounts).GetProperty(queryName.Target).GetValue(device);
                    JArray result = (JArray)JsonConvert.DeserializeObject(value);
                    var resultList = result.ToObject<List<int>>();

                        for (int i = 0; i < resultList.Count; i++)
                        {
                            List<DataPoint<int>> outputPointsList = new List<DataPoint<int>>();
                            DataPoint<int> outputPoint = new DataPoint<int>(device.Date, resultList[i]);
                            outputPointsList.Add(outputPoint);

                            DataPoint<int>[] outputPointArray = outputPointsList.ToArray();

                            DataSeries<int> currentOutputSeries = new DataSeries<int>(i.ToString(), outputPointArray);

                            outputSeries.Add(currentOutputSeries);
                        }
                }
            }
            return outputSeries;
        }

        public async Task<Table> GetThirtyDayOutputSeries(DateTime dataTo, DateTime dataFrom, TargetViewModel[] queryToPreform, string tablename)
        {

            var outputSeries = new List<DataSeries<int>>();

            var entityList = await _newTimeSeriesData.GetDataSeriesData(dataTo, dataFrom, tablename);

            var wiserHeatResultList = new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
            var auraConnectResultList = new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

            int maxIndex = entityList.Count - 1;

            var wiserHeatActiveConnection = (string) typeof(DeviceCounts)
                    .GetProperty("ThirtyDayWiserHeatActiveConnections").GetValue(entityList[maxIndex]);
            var auraConnectActiveConnection = (string) typeof(DeviceCounts)
                    .GetProperty("ThirtyDayAuraConnectActiveConnections").GetValue(entityList[maxIndex]);

            JArray wiserHeatResult = (JArray) JsonConvert.DeserializeObject(wiserHeatActiveConnection);
            JArray auraConnectResult = (JArray) JsonConvert.DeserializeObject(auraConnectActiveConnection);
            wiserHeatResultList = wiserHeatResult.ToObject<List<int>>();
            auraConnectResultList = auraConnectResult.ToObject<List<int>>();

            var dayNow = DateTime.Now;
            var table = new Table
            {
                columns = new List<Column>
                {
                    new Column {text = "Day", type = "string"},
                    new Column {text = "DataSource", type = "string"},
                    new Column {text = "Number", type = "number"},
                },
                rows = new List<object>
                {
                    new List<object> {dayNow.AddDays(-29).ToString("dd/MM"), "WiserHeat", wiserHeatResultList[0]}, new List<object> {dayNow.AddDays(-29).ToString("dd/MM"), "AuraConnect", auraConnectResultList[0]},
                    new List<object> {dayNow.AddDays(-28).ToString("dd/MM"), "WiserHeat", wiserHeatResultList[1]}, new List<object> {dayNow.AddDays(-28).ToString("dd/MM"), "AuraConnect", auraConnectResultList[1]},
                    new List<object> {dayNow.AddDays(-27).ToString("dd/MM"), "WiserHeat", wiserHeatResultList[2]}, new List<object> {dayNow.AddDays(-27).ToString("dd/MM"), "AuraConnect", auraConnectResultList[2]},
                    new List<object> {dayNow.AddDays(-26).ToString("dd/MM"), "WiserHeat", wiserHeatResultList[3]}, new List<object> {dayNow.AddDays(-26).ToString("dd/MM"), "AuraConnect", auraConnectResultList[3]},
                    new List<object> {dayNow.AddDays(-25).ToString("dd/MM"), "WiserHeat", wiserHeatResultList[4]}, new List<object> {dayNow.AddDays(-25).ToString("dd/MM"), "AuraConnect", auraConnectResultList[4]},
                    new List<object> {dayNow.AddDays(-24).ToString("dd/MM"), "WiserHeat", wiserHeatResultList[5]}, new List<object> {dayNow.AddDays(-24).ToString("dd/MM"), "AuraConnect", auraConnectResultList[5]},
                    new List<object> {dayNow.AddDays(-23).ToString("dd/MM"), "WiserHeat", wiserHeatResultList[6]}, new List<object> {dayNow.AddDays(-23).ToString("dd/MM"), "AuraConnect", auraConnectResultList[6]},
                    new List<object> {dayNow.AddDays(-22).ToString("dd/MM"), "WiserHeat", wiserHeatResultList[7]}, new List<object> {dayNow.AddDays(-22).ToString("dd/MM"), "AuraConnect", auraConnectResultList[7]},
                    new List<object> {dayNow.AddDays(-21).ToString("dd/MM"), "WiserHeat", wiserHeatResultList[8]}, new List<object> {dayNow.AddDays(-21).ToString("dd/MM"), "AuraConnect", auraConnectResultList[8]},
                    new List<object> {dayNow.AddDays(-20).ToString("dd/MM"), "WiserHeat", wiserHeatResultList[9]}, new List<object> {dayNow.AddDays(-20).ToString("dd/MM"), "AuraConnect", auraConnectResultList[9]},
                    new List<object> {dayNow.AddDays(-19).ToString("dd/MM"), "WiserHeat", wiserHeatResultList[10]}, new List<object> {dayNow.AddDays(-19).ToString("dd/MM"), "AuraConnect", auraConnectResultList[10]},
                    new List<object> {dayNow.AddDays(-18).ToString("dd/MM"), "WiserHeat", wiserHeatResultList[11]}, new List<object> {dayNow.AddDays(-18).ToString("dd/MM"), "AuraConnect", auraConnectResultList[11]},
                    new List<object> {dayNow.AddDays(-17).ToString("dd/MM"), "WiserHeat", wiserHeatResultList[12]}, new List<object> {dayNow.AddDays(-17).ToString("dd/MM"), "AuraConnect", auraConnectResultList[12]},
                    new List<object> {dayNow.AddDays(-16).ToString("dd/MM"), "WiserHeat", wiserHeatResultList[13]}, new List<object> {dayNow.AddDays(-16).ToString("dd/MM"), "AuraConnect", auraConnectResultList[13]},
                    new List<object> {dayNow.AddDays(-15).ToString("dd/MM"), "WiserHeat", wiserHeatResultList[14]}, new List<object> {dayNow.AddDays(-15).ToString("dd/MM"), "AuraConnect", auraConnectResultList[14]},
                    new List<object> {dayNow.AddDays(-14).ToString("dd/MM"), "WiserHeat", wiserHeatResultList[15]}, new List<object> {dayNow.AddDays(-14).ToString("dd/MM"), "AuraConnect", auraConnectResultList[15]},
                    new List<object> {dayNow.AddDays(-13).ToString("dd/MM"), "WiserHeat", wiserHeatResultList[16]}, new List<object> {dayNow.AddDays(-13).ToString("dd/MM"), "AuraConnect", auraConnectResultList[16]},
                    new List<object> {dayNow.AddDays(-12).ToString("dd/MM"), "WiserHeat", wiserHeatResultList[17]}, new List<object> {dayNow.AddDays(-12).ToString("dd/MM"), "AuraConnect", auraConnectResultList[17]},
                    new List<object> {dayNow.AddDays(-11).ToString("dd/MM"), "WiserHeat", wiserHeatResultList[18]}, new List<object> {dayNow.AddDays(-11).ToString("dd/MM"), "AuraConnect", auraConnectResultList[18]},
                    new List<object> {dayNow.AddDays(-10).ToString("dd/MM"), "WiserHeat", wiserHeatResultList[19]}, new List<object> {dayNow.AddDays(-10).ToString("dd/MM"), "AuraConnect", auraConnectResultList[19]},
                    new List<object> {dayNow.AddDays(-9).ToString("dd/MM"), "WiserHeat", wiserHeatResultList[20]}, new List<object> {dayNow.AddDays(-9).ToString("dd/MM"), "AuraConnect", auraConnectResultList[20]},
                    new List<object> {dayNow.AddDays(-8).ToString("dd/MM"), "WiserHeat", wiserHeatResultList[21]}, new List<object> {dayNow.AddDays(-8).ToString("dd/MM"), "AuraConnect", auraConnectResultList[21]},
                    new List<object> {dayNow.AddDays(-7).ToString("dd/MM"), "WiserHeat", wiserHeatResultList[22]}, new List<object> {dayNow.AddDays(-7).ToString("dd/MM"), "AuraConnect", auraConnectResultList[22]},
                    new List<object> {dayNow.AddDays(-6).ToString("dd/MM"), "WiserHeat", wiserHeatResultList[23]}, new List<object> {dayNow.AddDays(-6).ToString("dd/MM"), "AuraConnect", auraConnectResultList[23]},
                    new List<object> {dayNow.AddDays(-5).ToString("dd/MM"), "WiserHeat", wiserHeatResultList[24]}, new List<object> {dayNow.AddDays(-5).ToString("dd/MM"), "AuraConnect", auraConnectResultList[24]},
                    new List<object> {dayNow.AddDays(-4).ToString("dd/MM"), "WiserHeat", wiserHeatResultList[25]}, new List<object> {dayNow.AddDays(-4).ToString("dd/MM"), "AuraConnect", auraConnectResultList[25]},
                    new List<object> {dayNow.AddDays(-3).ToString("dd/MM"), "WiserHeat", wiserHeatResultList[26]}, new List<object> {dayNow.AddDays(-3).ToString("dd/MM"), "AuraConnect", auraConnectResultList[26]},
                    new List<object> {dayNow.AddDays(-2).ToString("dd/MM"), "WiserHeat", wiserHeatResultList[27]}, new List<object> {dayNow.AddDays(-2).ToString("dd/MM"), "AuraConnect", auraConnectResultList[27]},
                    new List<object> {dayNow.AddDays(-1).ToString("dd/MM"), "WiserHeat", wiserHeatResultList[28]}, new List<object> {dayNow.AddDays(-1).ToString("dd/MM"), "AuraConnect", auraConnectResultList[28]},
                    new List<object> {dayNow.AddDays(0).ToString("dd/MM"), "WiserHeat", wiserHeatResultList[29]}, new List<object> {dayNow.AddDays(0).ToString("dd/MM"), "AuraConnect", auraConnectResultList[29]},
                }

            };
                return table;
        }


        public async Task<List<DataSeries<int>>> GetNumberOfEachDeviceSeries(DateTime dataTo, DateTime dataFrom, TargetViewModel[] queryToPreform, string tablename)
        {

            var outputSeries = new List<DataSeries<int>>();

            var entityList = await _newTimeSeriesData.GetDataSeriesData(dataTo, dataFrom, tablename);


            List<DataPoint<int>> controllerOutputPointsList = new List<DataPoint<int>>();
            List<DataPoint<int>> iTRVsOutputPointsList = new List<DataPoint<int>>();
            List<DataPoint<int>> RTRsOutputPointsList = new List<DataPoint<int>>();
            List<DataPoint<int>> smartPlugOutputPointsList = new List<DataPoint<int>>();

            foreach (DeviceCounts device in entityList)
            {
                int controllerValue = (int)typeof(DeviceCounts).GetProperty("ControllerCount").GetValue(device);
                int iTRVsValue = (int)typeof(DeviceCounts).GetProperty("ItrvsCount").GetValue(device);
                int RTRsValue = (int)typeof(DeviceCounts).GetProperty("RoomStatCount").GetValue(device);
                int smartPlugValue = (int)typeof(DeviceCounts).GetProperty("HeimanSmartPlugCount").GetValue(device);

                DataPoint<int> controllerOutputPoint = new DataPoint<int>(device.Date, controllerValue);
                DataPoint<int> iTRVsOutputPoint = new DataPoint<int>(device.Date, iTRVsValue);
                DataPoint<int> RTRsOutputPoint = new DataPoint<int>(device.Date, RTRsValue);
                DataPoint<int> smartPlugOutputPoint = new DataPoint<int>(device.Date, smartPlugValue);

                controllerOutputPointsList.Add(controllerOutputPoint);
                iTRVsOutputPointsList.Add(iTRVsOutputPoint);
                RTRsOutputPointsList.Add(RTRsOutputPoint);
                smartPlugOutputPointsList.Add(smartPlugOutputPoint);
            }

                DataPoint<int>[] controllerOutputPointArray = controllerOutputPointsList.ToArray();
                DataPoint<int>[] iTRVsOutputPointArray = iTRVsOutputPointsList.ToArray();
                DataPoint<int>[] RTRsOutputPointArray = RTRsOutputPointsList.ToArray();
                DataPoint<int>[] smartPlugOutputPointArray = smartPlugOutputPointsList.ToArray();

                DataSeries<int> controllerOutputTimeSeries = new DataSeries<int>("Hubs", controllerOutputPointArray);
                DataSeries<int> iTRVsOutputTimeSeries = new DataSeries<int>("ITRVs", iTRVsOutputPointArray);
                DataSeries<int> RTRsOutputTimeSeries = new DataSeries<int>("RTRs", RTRsOutputPointArray);
                DataSeries<int> smartPlugOutputTimeSeries = new DataSeries<int>("Smart Plugs", smartPlugOutputPointArray);


                outputSeries.Add(controllerOutputTimeSeries);
                outputSeries.Add(iTRVsOutputTimeSeries);
                outputSeries.Add(RTRsOutputTimeSeries);
                outputSeries.Add(smartPlugOutputTimeSeries);

            return outputSeries;
        }

        public async Task<Table> GetOutputBarChart(DateTime dataTo, DateTime dataFrom, TargetViewModel[] queryToPreform, string tablename)
        {
            
            
            var outputSeries = new List<DataSeries<int>>();
            var entityList = await _newTimeSeriesData.GetDataSeriesData(dataTo, dataFrom, tablename);

            var wiserHeatResultList = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var auraConnectResultList = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            foreach (TargetViewModel queryName in queryToPreform)
            {

                foreach (DeviceCounts device in entityList)
                {
                    var wiserHeatValue = (string)typeof(DeviceCounts).GetProperty("WiserHeatBrandCount").GetValue(device);
                    var auraConnectValue = (string)typeof(DeviceCounts).GetProperty("AuraConnectBrandCount").GetValue(device);
                    JArray wiserHeatResult = (JArray)JsonConvert.DeserializeObject(wiserHeatValue);
                    JArray auraConnectResult = (JArray)JsonConvert.DeserializeObject(auraConnectValue);
                    wiserHeatResultList = wiserHeatResult.ToObject<List<int>>();
                    auraConnectResultList = auraConnectResult.ToObject<List<int>>();
                }
            }

            var table = new Table
            {
                columns = new List<Column>
                {
                    new Column {text = "Month", type = "string"},
                    new Column {text = "DataSource", type = "string"},
                    new Column {text = "Number", type = "number"},
                },
                rows = new List<object>
                {
                    new List<object>{"Jan","WiserHeat",wiserHeatResultList[0]}, new List<object>{ "Jan", "AuraConnect",auraConnectResultList[0]},
                    new List<object>{ "Feb", "WiserHeat", wiserHeatResultList[1]}, new List<object>{"Feb", "AuraConnect", auraConnectResultList[1]},
                    new List<object>{ "Mar", "WiserHeat", wiserHeatResultList[2]}, new List<object>{ "Mar", "AuraConnect", auraConnectResultList[2]},
                    new List<object>{"Apr", "WiserHeat", wiserHeatResultList[3]}, new List<object>{ "Apr", "AuraConnect", auraConnectResultList[3]},
                    new List<object>{"May", "WiserHeat", wiserHeatResultList[4]}, new List<object>{ "May", "AuraConnect", auraConnectResultList[4]},
                    new List<object>{ "Jun", "WiserHeat", wiserHeatResultList[5]}, new List<object>{ "Jun", "AuraConnect", auraConnectResultList[5]},
                    new List<object>{ "Jul", "WiserHeat", wiserHeatResultList[6]}, new List<object>{ "Jul", "AuraConnect", auraConnectResultList[6]},
                    new List<object>{ "Aug", "WiserHeat", wiserHeatResultList[7]}, new List<object>{ "Aug", "AuraConnect", auraConnectResultList[7]},
                    new List<object>{ "Sep", "WiserHeat", wiserHeatResultList[8]}, new List<object>{ "Sep", "AuraConnect", auraConnectResultList[8]},
                    new List<object>{ "Oct", "WiserHeat", wiserHeatResultList[9]}, new List<object>{ "Oct", "AuraConnect", auraConnectResultList[9]},
                    new List<object>{ "Nov", "WiserHeat", wiserHeatResultList[10]}, new List<object>{ "Nov", "AuraConnect", auraConnectResultList[10]},
                    new List<object>{ "Dec", "WiserHeat", wiserHeatResultList[11]}, new List<object>{ "Dec", "AuraConnect", auraConnectResultList[11]},
                }
            };
            
            return table;
    
        }
        
                public async Task<List<DataSeries<int>>> GetMostCommonRecentProblem( TargetViewModel[] queryToPreform, string tablename)
                {

                    var outputSeries = new List<DataSeries<int>>();

                    var entityList = await _newTimeSeriesData.GetMostCommonRecentProblemsData(tablename);

                    
                    for (int i = 0; i < 5; i++)
                    {

                        DataPoint<int> outputPoint = new DataPoint<int>(entityList[i].Date, entityList[i].ProblemCount);
                        List<DataPoint<int>> outputPointsList = new List<DataPoint<int>>();
                        outputPointsList.Add(outputPoint);
                        DataPoint<int>[] outputPointArray = outputPointsList.ToArray();
                        DataSeries<int> outputProblem = new DataSeries<int>(entityList[i].ProblemName, outputPointArray);
                        outputSeries.Add(outputProblem);
                    }

                    return outputSeries;
                }
        
        public async Task<List<DataSeries<int>>> GetOutputEcoIQTiming(DateTime dataTo, DateTime dataFrom, TargetViewModel[] queryToPreform, string tablename)
        {

            var outputSeries = new List<DataSeries<int>>();

            var entityList = await _newTimeSeriesData.GetDataEcoIQTimeSeriesData(dataTo, dataFrom, tablename);

            
                List<DataPoint<int>> ecoModeOutputPointsList = new List<DataPoint<int>>();
                List<DataPoint<int>> scheduleOutputPointsList = new List<DataPoint<int>>();
                List<DataPoint<int>> telemetryOutputPointsList = new List<DataPoint<int>>();

                foreach (EcoIQTimingCount timing in entityList)
                {
                    int ecoModeValue = (int)typeof(EcoIQTimingCount).GetProperty("EcoModeEnabledMessagesProcessedInLastMinute").GetValue(timing);
                    int scheduleValue = (int)typeof(EcoIQTimingCount).GetProperty("ScheduleUpdateMessagesProcessedInLastMinute").GetValue(timing);
                    int telemetry = (int)typeof(EcoIQTimingCount).GetProperty("TelemetryMessagesProcessedInLastMinute").GetValue(timing);

                    DataPoint<int> ecoModeOutputPoint = new DataPoint<int>(timing.Date, ecoModeValue);
                    DataPoint<int> scheduleOutputPoint = new DataPoint<int>(timing.Date, scheduleValue);
                    DataPoint<int> telemetryOutputPoint = new DataPoint<int>(timing.Date, telemetry);

                    ecoModeOutputPointsList.Add(ecoModeOutputPoint);
                    scheduleOutputPointsList.Add(scheduleOutputPoint);
                    telemetryOutputPointsList.Add(telemetryOutputPoint);
                }

                DataPoint<int>[] ecoModeOutputPointArray = ecoModeOutputPointsList.ToArray();
                DataPoint<int>[] scheduleOutputPointArray = scheduleOutputPointsList.ToArray();
                DataPoint<int>[] telemetryOutputPointArray = telemetryOutputPointsList.ToArray();

                DataSeries<int> ecoModeOutputTimeSeries = new DataSeries<int>("EcoModeEnabledMessagesProcessedInLastMinute", ecoModeOutputPointArray);
                DataSeries<int> scheduleOutputTimeSeries = new DataSeries<int>("ScheduleUpdateMessagesProcessedInLastMinute", scheduleOutputPointArray);
                DataSeries<int> telemetryOutputTimeSeries = new DataSeries<int>("TelemetryMessagesProcessedInLastMinute", telemetryOutputPointArray);



                outputSeries.Add(ecoModeOutputTimeSeries);
                outputSeries.Add(scheduleOutputTimeSeries);
                outputSeries.Add(telemetryOutputTimeSeries);
            

            return outputSeries;
        }
    }
}
