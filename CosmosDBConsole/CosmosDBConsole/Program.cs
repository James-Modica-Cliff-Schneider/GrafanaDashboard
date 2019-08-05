using System;


namespace CosmosDBConsole
{
    class Program
    {
 
        
        static void Main()
        {
            

            DataSamples dataSamples = new DataSamples();

            dataSamples.RunSamples().Wait();

            
        }


    }
}
