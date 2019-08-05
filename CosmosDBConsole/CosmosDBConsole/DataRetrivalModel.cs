using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CosmosDBConsole.Model;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace CosmosDBConsole
{
    public class DataRetrivalModel
    {
        private List<WiserAuraCount> _brandTwelveMonthTableList = new List<WiserAuraCount>();
        private List<BrandActiveConnections> _brandThirtyDayTableList = new List<BrandActiveConnections>();
        private CloudTable _table;

        private const string EndpointUrl = "https://prod-wo-wh.documents.azure.com:443/";

        private const string PrimaryKey =
            "NN1RyAiUeFoRZLLajVg8l5RRDYtYn0SBGCr0fTuRvxBcGRe3ZVvPBpenRGEoIeWyTZkvCgeAtI6v1m3kE1HZxg==";

        private static readonly DateTime UnixEpochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private string databaseId = "WiserHeat";

        private string collectionId = "CurrentDataModels";

        private DocumentClient _client;

       
        public void DataModelRetrieval()
        {
            _client = new DocumentClient(new Uri(EndpointUrl), PrimaryKey);

            
        }



        public async Task<List<dynamic>> ReadConnectedDevicesCount()
        {
            DataModelRetrieval();


            try
            {
                var sqlExpression = $"SELECT VALUE COUNT(f.ProductIdentifier) FROM c JOIN f IN c.Data.domain.Device";
                var resultList = await _client.CreateDocumentQuery(UriFactory.CreateDocumentCollectionUri(databaseId, collectionId), sqlExpression, new FeedOptions { EnableCrossPartitionQuery = true }).
                    ToListAsync();



                return resultList;
            }
            catch (Exception ex)
            {
                return new List<dynamic> { ex.Message };
            }
        }

        public async Task<List<dynamic>> ReadDeviceCount(string deviceName)
        {
            DataModelRetrieval();



            try
            {
                var sqlExpression = $"SELECT VALUE COUNT(f.ProductIdentifier) FROM c JOIN f IN c.Data.domain.Device WHERE f.ProductIdentifier = '" + deviceName +"'";
                var resultList = await _client.CreateDocumentQuery(UriFactory.CreateDocumentCollectionUri(databaseId, collectionId), sqlExpression, new FeedOptions { EnableCrossPartitionQuery = true }).
                    ToListAsync();


                
                return resultList;
            }
            catch (Exception ex)
            {
                return new List<dynamic> { ex.Message };
            }
        }

        public async Task<List<dynamic>> ReadHubTypeCount(string hubTypeName)
        {
            DataModelRetrieval();


            try
            {
                var sqlExpression = $"SELECT VALUE COUNT(f.ModelIdentifier) FROM c JOIN f IN c.Data.domain.Device WHERE f.ProductIdentifier = 'Controller' AND f.ModelIdentifier = '"+ hubTypeName +"'";
                var resultList = await _client.CreateDocumentQuery(UriFactory.CreateDocumentCollectionUri(databaseId, collectionId), sqlExpression, new FeedOptions { EnableCrossPartitionQuery = true }).
                    ToListAsync();


                return resultList;
            }
            catch (Exception ex)
            {
                return new List<dynamic> { ex.Message };
            }
        }


        public async Task<List<int>> ReadWiserAuraCount(string brandName)
        {
            DataModelRetrieval();


            _table = await Common.ConnectTableAsync("DataTimeSeries");

            var entityList = await Utils.DataTimeSeriesPartitionRangeQueryAsync(_table, "Common", DateTime.Now, DateTime.Now.AddDays(-7));
            _brandTwelveMonthTableList = entityList
                .Select(entity => new WiserAuraCount(entity.WiserHeatBrandCount, entity.AuraConnectBrandCount))
                .ToList();

            
                var sqlExpression = $"SELECT VALUE COUNT(c.Data.domain.System.BrandName) FROM c WHERE c.Data.domain.System.BrandName = '" + brandName + "'";
                var resultSqlList = await _client.CreateDocumentQuery(UriFactory.CreateDocumentCollectionUri(databaseId, collectionId), sqlExpression, new FeedOptions { EnableCrossPartitionQuery = true }).
                    ToListAsync();

                var resultList = FormatWiserAuraCounts(_brandTwelveMonthTableList, resultSqlList, brandName);
                
                return resultList;
            
        }

        public async Task<List<dynamic>> ReadFeatureUsage(string feature)
        {
            DataModelRetrieval();

            var sqlExpression =
                $"SELECT VALUE COUNT(c.Data.domain.System." + feature + ") FROM c WHERE c.Data.domain.System." + feature + " = true";
            
            var resultList = await _client.CreateDocumentQuery(UriFactory.CreateDocumentCollectionUri(databaseId, collectionId), sqlExpression, new FeedOptions { EnableCrossPartitionQuery = true }).
                ToListAsync();

            return resultList;
        }

        public async Task<int> ReadOpenWindowDetectionUsage()
        {
            DataModelRetrieval();

            var sqlExpression =
                $"SELECT DISTINCT c.MacAddress FROM c JOIN f IN c.Data.domain.Room WHERE f.WindowDetectionActive = true";

            var resultList = await _client.CreateDocumentQuery(UriFactory.CreateDocumentCollectionUri(databaseId, collectionId), sqlExpression, new FeedOptions { EnableCrossPartitionQuery = true }).
                ToListAsync();

            return resultList.Count;
        }



        public async Task<List<int>> ReadPopulationCount(string product)
        {
            DataModelRetrieval();
            
            var sqlExpression =
                $"SELECT ARRAY(SELECT VALUE COUNT(f.ProductIdentifier) FROM f IN c.Data.domain.Device WHERE f.ProductIdentifier = '" +
                product + "') AS ProdCount FROM c WHERE c.Component = 'domain'";

            var resultList = await _client
                .CreateDocumentQuery<JObject>(UriFactory.CreateDocumentCollectionUri(databaseId, collectionId),
                    sqlExpression, new FeedOptions {EnableCrossPartitionQuery = true}).ToListAsync();

            return FormatPopulationCounts(resultList.Cast<JObject>());
        }

        public async Task<List<int>> ReadAllPopulationCount()
        {
            DataModelRetrieval();

            var sqlExpression =
                $"SELECT ARRAY(SELECT VALUE COUNT(f.ProductIdentifier) FROM f IN c.Data.domain.Device) AS ProdCount FROM c WHERE c.Component = 'domain'";

            var resultList = await _client
                .CreateDocumentQuery<JObject>(UriFactory.CreateDocumentCollectionUri(databaseId, collectionId),
                    sqlExpression, new FeedOptions { EnableCrossPartitionQuery = true }).ToListAsync();

            return FormatPopulationCounts(resultList.Cast<JObject>());
        }

        public async Task<List<int>> ReadThirtyDayActiveConnections(string brandName)
        {
            DataModelRetrieval();


            _table = await Common.ConnectTableAsync("DataTimeSeries");

            var entityList = await Utils.DataTimeSeriesPartitionRangeQueryAsync(_table, "Common", DateTime.Now, DateTime.Now.AddDays(-7));
            _brandThirtyDayTableList = entityList
                .Select(entity => new BrandActiveConnections(entity.ThirtyDayWiserHeatActiveConnections, entity.ThirtyDayAuraConnectActiveConnections))
                .ToList();


            var sqlExpression = $"SELECT VALUE COUNT(c.Data.domain.System.BrandName) FROM c WHERE c.Data.domain.System.BrandName = '" + brandName + "'";
            var resultSqlList = await _client.CreateDocumentQuery(UriFactory.CreateDocumentCollectionUri(databaseId, collectionId), sqlExpression, new FeedOptions { EnableCrossPartitionQuery = true }).
                ToListAsync();

            var resultList = FormatThirtDayActiveConnections(_brandThirtyDayTableList, resultSqlList, brandName);

            return resultList;
        }

        public static List<int> FormatWiserAuraCounts(List<WiserAuraCount> existingValuesToAdd, List<dynamic> valueToAdd, string brandName)
        {
            int value = Convert.ToInt32(valueToAdd[0]);

            var existingValues = (string)typeof(WiserAuraCount).GetProperty(brandName).GetValue(existingValuesToAdd[0]);

            var resultList = new List<int>();

            if (existingValues == null)
            {
                for (int i = 0; i < 12; i++)
                {
                    resultList.Add(0);
                }
            }
            else
            {
                JArray result = (JArray)JsonConvert.DeserializeObject(existingValues);
                resultList = result.ToObject<List<int>>();

            }


            int monthValue = DateTime.Now.Month;

            resultList[(monthValue - 1)] = value;

            return resultList;
        }

        public static List<int> FormatThirtDayActiveConnections(List<BrandActiveConnections> existingValuesToAdd, List<dynamic> valueToAdd, string brandName)
        {
            int value = Convert.ToInt32(valueToAdd[0]);

            var existingValues = (string)typeof(BrandActiveConnections).GetProperty(brandName).GetValue(existingValuesToAdd[0]);

            var resultList = new List<int>();

            if (existingValues == null)
            {
                for (int i = 0; i < 29; i++)
                {
                    resultList.Add(0);
                }

                resultList.Add(value);
            }
            else
            {
                JArray result = (JArray)JsonConvert.DeserializeObject(existingValues);
                resultList = result.ToObject<List<int>>();

                var newResultList = new List<int>();

                for (int i = 0; i < 29; i++)
                {
                    newResultList.Add(resultList[i + 1]);
                }

                newResultList.Add(value);

                resultList = newResultList;
            }
            
            return resultList;
        }

        public static List<int> FormatPopulationCounts(IEnumerable<JObject> resultList)
        {
            var countList = new List<int>();
            for (int i = 0; i < 33; i++)
            {
                countList.Add(0);
            }
            
            foreach (var jObject in resultList)
            {
                var value = GetProdCountFromJObject(jObject);
                if (value < 33)
                {
                    countList[value]++;
                }
                else
                {
                    
                }
            }

            return countList;
        }

       
        private static int GetProdCountFromJObject(JObject jObject)
        {
            try
            {
                var result = jObject.Property("ProdCount").Value.First as JValue;
                return Convert.ToInt32(result.Value);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException)
                {
                    throw new ArgumentException("Expected JObject with ProdCount property ",jObject.ToString());
                }
                else if (ex is NullReferenceException)
                {
                    throw new NullReferenceException("Expected JObject with ProdCount property ");
                }
                else if (ex is InvalidOperationException)
                {
                    throw new InvalidOperationException("{\"ProdCount\": 3}");
                }
                else
                {
                    throw new Exception();
                }
            }
        }
    }
}

