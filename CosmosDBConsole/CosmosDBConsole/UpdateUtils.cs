using CosmosDBConsole.Model;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Threading.Tasks;

namespace CosmosDBConsole
{
    class CRUDUtils
    {
        public static async Task<DataEntity> InsertOrMergeEntityAsync(CloudTable table, DataEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                // Create the InsertOrReplace table operation
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity);

                // Execute the operation.
                TableResult result = await table.ExecuteAsync(insertOrMergeOperation);
                DataEntity insertedTimeSeries = result.Result as DataEntity;

                // Get the request units consumed by the current operation. RequestCharge of a TableResult is only applied to Azure CosmoS DB 
                if (result.RequestCharge.HasValue)
                {
                    Console.WriteLine("Request Charge of InsertOrMerge Operation: " + result.RequestCharge);
                }

                return insertedTimeSeries;
            }
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
        }

        

        

    }
}
