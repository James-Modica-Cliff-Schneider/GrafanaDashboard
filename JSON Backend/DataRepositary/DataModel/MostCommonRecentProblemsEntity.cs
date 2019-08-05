using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Cosmos.Table;

namespace DataRepository.DataModel
{
    public class MostCommonRecentProblemsEntity : TableEntity
    {
        public MostCommonRecentProblemsEntity()
        {
        }

        public MostCommonRecentProblemsEntity(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }
        public DateTime TimeStamp { get; set; }
        public int Count { get; set; }
    }
}
