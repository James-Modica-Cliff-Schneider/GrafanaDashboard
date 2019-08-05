using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataRepository.DataModel;
using Microsoft.Azure.Cosmos.Table;

namespace DataRepository
{
    public class Utils
    {
        public static async Task<List<DataTimeSeriesEntity>> DataTimeSeriesPartitionRangeQueryAsync(CloudTable table, string partitionKey, DateTime to, DateTime from)
        {
            List<DataTimeSeriesEntity> returnList = new List<DataTimeSeriesEntity>();

            
            string greatestTimeTick = (long.MaxValue - to.Ticks).ToString();

            
            string lowestTimeTick = (long.MaxValue - from.Ticks).ToString();

            try
            {
                // Create the range query using the fluid API 
                TableQuery<DataTimeSeriesEntity> rangeQuery = new TableQuery<DataTimeSeriesEntity>().Where(
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
                    TableQuerySegment<DataTimeSeriesEntity> segment = await table.ExecuteQuerySegmentedAsync(rangeQuery, token);

                  

                    // Save the continuation token for the next call to ExecuteQuerySegmentedAsync
                    token = segment.ContinuationToken;

                    // Add entity to list of entity for each entity returned.
                    foreach (DataTimeSeriesEntity entity in segment)
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

        public static async Task<List<DeviceLocationsAndStatusEntity>> DeviceLocationAndStatusPartitionRangeQueryAsync(CloudTable table, string partitionKey, DateTime to, DateTime from)
        {
            List<DeviceLocationsAndStatusEntity> returnList = new List<DeviceLocationsAndStatusEntity>();


            string greatestTimeTick = (long.MaxValue - to.Ticks).ToString();


            string lowestTimeTick = (long.MaxValue - from.Ticks).ToString();

            try
            {
                // Create the range query using the fluid API 
                TableQuery<DeviceLocationsAndStatusEntity> rangeQuery = new TableQuery<DeviceLocationsAndStatusEntity>().Where(
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
                    TableQuerySegment<DeviceLocationsAndStatusEntity> segment = await table.ExecuteQuerySegmentedAsync(rangeQuery, token);



                    // Save the continuation token for the next call to ExecuteQuerySegmentedAsync
                    token = segment.ContinuationToken;

                    // Add entity to list of entity for each entity returned.
                    foreach (DeviceLocationsAndStatusEntity entity in segment)
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

        public static async Task<List<EcoIQTimingEntity>> EcoIQTimingPartitionRangeQueryAsync(CloudTable table, string partitionKey, DateTime to, DateTime from)
        {
            List<EcoIQTimingEntity> returnList = new List<EcoIQTimingEntity>();


            string greatestTimeTick = ToReverseTicks(to).ToString();


            string lowestTimeTick = ToReverseTicks(from).ToString();

            try
            {
                // Create the range query using the fluid API 
                TableQuery<EcoIQTimingEntity> rangeQuery = new TableQuery<EcoIQTimingEntity>().Where(
                    TableQuery.CombineFilters(
                            TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey),
                            TableOperators.And,
                            TableQuery.CombineFilters(
                                TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.GreaterThanOrEqual, greatestTimeTick),
                                TableOperators.And,
                                TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.LessThanOrEqual, lowestTimeTick))));

                // Request 1000 results at a time from the server. 
                TableContinuationToken token = null;
                rangeQuery.TakeCount = 1000;

                do
                {
                    // Execute the query, passing in the continuation token.
                    // The first time this method is called, the continuation token is null. If there are more results, the call
                    // populates the continuation token for use in the next call.
                    TableQuerySegment<EcoIQTimingEntity> segment = await table.ExecuteQuerySegmentedAsync(rangeQuery, token);



                    // Save the continuation token for the next call to ExecuteQuerySegmentedAsync
                    token = segment.ContinuationToken;

                    // Add entity to list of entity for each entity returned.
                    foreach (EcoIQTimingEntity entity in segment)
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

        public static async Task<List<MostCommonRecentProblemsEntity>> MostCommonRecentproblemPartitionRangeQueryAsync(CloudTable table, string partitionKey)
        {
            List<MostCommonRecentProblemsEntity> returnList = new List<MostCommonRecentProblemsEntity>();

            

            try
            {
                // Create the range query using the fluid API 
                TableQuery<MostCommonRecentProblemsEntity> rangeQuery =
                    new TableQuery<MostCommonRecentProblemsEntity>().Where(
                        TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));
                      

                            // Request 30 results at a time from the server. 
                TableContinuationToken token = null;
                rangeQuery.TakeCount = 30;

                do
                {
                    // Execute the query, passing in the continuation token.
                    // The first time this method is called, the continuation token is null. If there are more results, the call
                    // populates the continuation token for use in the next call.
                    TableQuerySegment<MostCommonRecentProblemsEntity> segment = await table.ExecuteQuerySegmentedAsync(rangeQuery, token);



                    // Save the continuation token for the next call to ExecuteQuerySegmentedAsync
                    token = segment.ContinuationToken;

                    // Add entity to list of entity for each entity returned.
                    foreach (MostCommonRecentProblemsEntity entity in segment)
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

        public static DateTime FromReverseTicks( long ticks)
            => new DateTime(DateTime.MaxValue.Ticks - ticks);

        public static long ToReverseTicks( DateTime dateTime)
            => DateTime.MaxValue.Ticks - dateTime.Ticks;
    }
}
