using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class EcoIQTimingCount
    {
        public DateTime Date { get; set; }
        public int EcoModeEnabledMessagesProcessedInLastMinute { get; set; }
        public int ScheduleUpdateMessagesProcessedInLastMinute { get; set; }
        public int TelemetryMessagesProcessedInLastMinute { get; set; }


        public EcoIQTimingCount(DateTime date, int ecoModeEnabledMessagesProcessedInLastMinute, int scheduleUpdateMessagesProcessedInLastMinute, int telemetryMessagesProcessedInLastMinute)
        {
            Date = date;
            EcoModeEnabledMessagesProcessedInLastMinute = ecoModeEnabledMessagesProcessedInLastMinute;
            ScheduleUpdateMessagesProcessedInLastMinute = scheduleUpdateMessagesProcessedInLastMinute;
            TelemetryMessagesProcessedInLastMinute = telemetryMessagesProcessedInLastMinute;

        }
    }
}
