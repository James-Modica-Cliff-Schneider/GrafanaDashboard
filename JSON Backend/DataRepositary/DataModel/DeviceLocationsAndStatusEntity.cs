using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Cosmos.Table;

namespace DataRepository.DataModel
{
   public class DeviceLocationsAndStatusEntity : TableEntity
   {
       public DeviceLocationsAndStatusEntity()
       {
       }

       public DeviceLocationsAndStatusEntity(string partitionKey, string rowKey)
       {
           PartitionKey = partitionKey;
           RowKey = rowKey;
       }

       public DateTime TimeStamp { get; set; }
       public bool HasError { get; set; }
       public bool IsInEcoModePeriod { get; set; }
       public bool EcoModeEnabled { get; set; }
       public double Longitude { get; set; }
       public double Latitude { get; set; }
    }
}
