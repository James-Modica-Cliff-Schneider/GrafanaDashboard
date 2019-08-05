using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CosmosDBConsole.Model;
using Microsoft.Azure.Cosmos.Table;

namespace CosmosDBConsole
{
    class Utils
    {
        public static async Task<List<DataEntity>> DataTimeSeriesPartitionRangeQueryAsync(CloudTable table, string partitionKey, DateTime to, DateTime from)
        {
            List<DataEntity> returnList = new List<DataEntity>();


            string greatestTimeTick = (long.MaxValue - to.Ticks).ToString();


            string lowestTimeTick = (long.MaxValue - from.Ticks).ToString();

            try
            {
                // Create the range query using the fluid API 
                TableQuery<DataEntity> rangeQuery = new TableQuery<DataEntity>().Where(
                    TableQuery.CombineFilters(
                            TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey),
                            TableOperators.And,
                            TableQuery.CombineFilters(
                                TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.GreaterThanOrEqual, greatestTimeTick),
                                TableOperators.And,
                                TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.LessThanOrEqual, lowestTimeTick))));

                // Request 30 results at a time from the server. 
                TableContinuationToken token = null;
                rangeQuery.TakeCount = 30;

                do
                {
                    // Execute the query, passing in the continuation token.
                    // The first time this method is called, the continuation token is null. If there are more results, the call
                    // populates the continuation token for use in the next call.
                    TableQuerySegment<DataEntity> segment = await table.ExecuteQuerySegmentedAsync(rangeQuery, token);



                    // Save the continuation token for the next call to ExecuteQuerySegmentedAsync
                    token = segment.ContinuationToken;

                    // Add entity to list of entity for each entity returned.
                    foreach (DataEntity entity in segment)
                    {
                        returnList.Add(entity);
                    }

                    Console.WriteLine();
                }
                while (token != null);
            }
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }

            return returnList;
        }

    }
}
