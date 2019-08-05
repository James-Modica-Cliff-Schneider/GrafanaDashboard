using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;


namespace DataRepository
{
    public class Common
    {
        

        public static CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
        {
            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the application.");
                throw;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
                Console.ReadLine();
                throw;
            }

            return storageAccount;
        }


        public static async Task<CloudTable> ConnectTableAsync(string tableName)
        {
            string storageConnectionString;
            CloudStorageAccount storageAccount = null;

            if (tableName == "DataTimeSeries")
            {
                 storageConnectionString = AppSettings.LoadAppSettings().StorageConnectionString;

            }
            else
            {
                storageConnectionString = AppSettings.LoadAppSettings().EcoIQConnectionString;
            }

            storageAccount = CreateStorageAccountFromConnectionString(storageConnectionString);

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());

            

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference(tableName);
         
            return table;
        }
    }
}
