using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Cosmos.Table;

namespace DataRepository.DataModel
{
    public class EcoIQTimingEntity : TableEntity
    {
        public EcoIQTimingEntity()
        {
        }

        public EcoIQTimingEntity(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }

        public DateTime TimeStamp { get; set; }
        public int EcoModeEnabledMessagesProcessedInLastMinute { get; set; }
        public int TelemetryMessageProcessedInLastMinute { get; set; }
        public int ScheduleUpdateMessagesProcessedInLastMinute { get; set; }

        public DateTime RowDateTime()
        {
            return new DateTime(DateTime.MaxValue.Ticks - Convert.ToInt64(RowKey));
        }
    }
}
