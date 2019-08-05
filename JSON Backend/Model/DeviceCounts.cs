using System;

namespace Model
{
    public class DeviceCounts
    {
        protected bool Equals(DeviceCounts other)
        {
            return Date.Equals(other.Date) && ConnectedDeviceCount == other.ConnectedDeviceCount && ControllerCount == other.ControllerCount && HeatingActuatorCount == other.HeatingActuatorCount && HeimanSmartPlugCount == other.HeimanSmartPlugCount && ItrvsCount == other.ItrvsCount && LoadActuatorCount == other.LoadActuatorCount && OwonSmartPlugCount == other.OwonSmartPlugCount && RoomStatCount == other.RoomStatCount && UnderFloorHeatingCount == other.UnderFloorHeatingCount && WT724R1S0902Count == other.WT724R1S0902Count && WT714R1S0902Count == other.WT714R1S0902Count && WT734R1S0902Count == other.WT734R1S0902Count && WT704R1S30S2Count == other.WT704R1S30S2Count && WT714R1S30S2Count == other.WT714R1S30S2Count && WT704R1A580HCount == other.WT704R1A580HCount && WT714R1A580HCount == other.WT714R1A580HCount && WT704R1A30S4Count == other.WT704R1A30S4Count;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DeviceCounts) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Date.GetHashCode();
                hashCode = (hashCode * 397) ^ ConnectedDeviceCount;
                hashCode = (hashCode * 397) ^ ControllerCount;
                hashCode = (hashCode * 397) ^ HeatingActuatorCount;
                hashCode = (hashCode * 397) ^ HeimanSmartPlugCount;
                hashCode = (hashCode * 397) ^ ItrvsCount;
                hashCode = (hashCode * 397) ^ LoadActuatorCount;
                hashCode = (hashCode * 397) ^ OwonSmartPlugCount;
                hashCode = (hashCode * 397) ^ RoomStatCount;
                hashCode = (hashCode * 397) ^ UnderFloorHeatingCount;
                hashCode = (hashCode * 397) ^ WT724R1S0902Count;
                hashCode = (hashCode * 397) ^ WT714R1S0902Count;
                hashCode = (hashCode * 397) ^ WT704R1S1804Count;
                hashCode = (hashCode * 397) ^ WT734R1S0902Count;
                hashCode = (hashCode * 397) ^ WT704R1S30S2Count;
                hashCode = (hashCode * 397) ^ WT714R1S30S2Count;
                hashCode = (hashCode * 397) ^ WT704R1A580HCount;
                hashCode = (hashCode * 397) ^ WT714R1A580HCount;
                hashCode = (hashCode * 397) ^ WT704R1A30S4Count;
                return hashCode;
            }
        }

        public DateTime Date { get; set; }
        public int ConnectedDeviceCount { get; set; }
        public int ControllerCount { get; set; }
        public int HeatingActuatorCount { get; set; }
        public int HeimanSmartPlugCount { get; set; }
        public int ItrvsCount { get; set; }
        public int LoadActuatorCount { get; set; }
        public int OwonSmartPlugCount { get; set; }
        public int RoomStatCount { get; set; }
        public int UnderFloorHeatingCount { get; set; }
        public int WT724R1S0902Count { get; set; }
        public int WT714R1S0902Count { get; set; }
        public int WT704R1S1804Count { get; set; }
        public int WT734R1S0902Count { get; set; }
        public int WT704R1S30S2Count { get; set; }
        public int WT714R1S30S2Count { get; set; }
        public int WT704R1A580HCount { get; set; }
        public int WT714R1A580HCount { get; set; }
        public int WT704R1A30S4Count { get; set; }
        public string WiserHeatBrandCount { get; set; }
        public string AuraConnectBrandCount { get; set; }
        public int EcoModeUsage { get; set; }
        public int ComfortModeUsage { get; set; }
        public int OpenWindowDetectionUsage { get; set; }
        public string ThirtyDayWiserHeatActiveConnections { get; set; }
        public string ThirtyDayAuraConnectActiveConnections { get; set; }

        public string ITRVPopulationCount { get; set; }
        public string HeimanSmartPlugPopulationCount { get; set; }
        public string RoomStatPopulationCount { get; set; }
        public string UnderFloorHeatingPopulationCount { get; set; }
        public string AllDevicesPopulationCount { get; set; }

        public DeviceCounts()
        {

        }

        public DeviceCounts(DateTime date, string connectedDeviceCount, string controllerCount, string heatingActuatorCount, string heimanSmartPlugCount, string itrvsCount, string loadActuatorCount, string owonSmartPlugCount, string roomStatCount, string underFloorHeating, string wT724R1S0902, string wT714R1S0902, string wT704R1S1804, string wT734R1S0902, string wT704R1S30S2, string wT714R1S30S2, string wT704R1A580H, string wT714R1A580H, string wT704R1A30S4, string iTRVPopulationCount, string heimanSmartPlugPopulationCount, string roomStatPopulationCount,string underFloorHeatingPopulationCount, string allDevicesPopulationCount, string wiserHeatBrandCount, string auraConnectBrandCount, string ecoModeUsage, string comfortModeUsage, string thirtyDayWiserHeatActiveConnections, string thirtyDayAuraConnectActiveConnections, string openWindowDetectionUsage)
        {
            Date = date;
            ConnectedDeviceCount = int.Parse(connectedDeviceCount);
            ControllerCount = int.Parse(controllerCount);
            HeatingActuatorCount = int.Parse(heatingActuatorCount);
            HeimanSmartPlugCount = int.Parse(heimanSmartPlugCount);
            ItrvsCount = int.Parse(itrvsCount);
            LoadActuatorCount = int.Parse(loadActuatorCount);
            OwonSmartPlugCount = int.Parse(owonSmartPlugCount);
            RoomStatCount = int.Parse(roomStatCount);
            UnderFloorHeatingCount = int.Parse(underFloorHeating);
            WT724R1S0902Count = int.Parse(wT724R1S0902);
            WT714R1S0902Count = int.Parse(wT714R1S0902);
            WT704R1S1804Count = int.Parse(wT704R1S1804);
            WT734R1S0902Count = int.Parse(wT734R1S0902);
            WT704R1S30S2Count = int.Parse(wT704R1S30S2);
            WT714R1S30S2Count = int.Parse(wT714R1S30S2);
            WT704R1A580HCount = int.Parse(wT704R1A580H);
            WT714R1A580HCount = int.Parse(wT714R1A580H);
            WT704R1A30S4Count = int.Parse(wT704R1A30S4);
            WiserHeatBrandCount = wiserHeatBrandCount;
            AuraConnectBrandCount = auraConnectBrandCount;
            EcoModeUsage = int.Parse(ecoModeUsage);
            ComfortModeUsage = int.Parse(comfortModeUsage);
            OpenWindowDetectionUsage = int.Parse(openWindowDetectionUsage);
            ThirtyDayWiserHeatActiveConnections = thirtyDayWiserHeatActiveConnections;
            ThirtyDayAuraConnectActiveConnections = thirtyDayAuraConnectActiveConnections;

            ITRVPopulationCount = iTRVPopulationCount;
            HeimanSmartPlugPopulationCount = heimanSmartPlugPopulationCount;
            RoomStatPopulationCount = roomStatPopulationCount;
            UnderFloorHeatingPopulationCount = underFloorHeatingPopulationCount;
            AllDevicesPopulationCount = allDevicesPopulationCount;
        }

        
    }
}
