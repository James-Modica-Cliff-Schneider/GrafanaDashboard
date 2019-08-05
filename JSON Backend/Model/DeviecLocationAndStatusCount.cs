using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class DeviecLocationAndStatusCount
    {
        public string MacAddress { get; set; }
        public DateTime Date { get; set; }
        public bool HasError { get; set; }
        public bool IsInEcoModePeriod { get; set; }
        public bool EcoModeEnabled { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public DeviecLocationAndStatusCount(string macAddress, DateTime date, bool hasError, bool isInEcoModePeriod, bool ecoModeEnabled, double longitude, double latitude)
        {
            MacAddress = macAddress;
            Date = date;
            HasError = hasError;
            IsInEcoModePeriod = isInEcoModePeriod;
            EcoModeEnabled = ecoModeEnabled;
            Longitude = longitude;
            Latitude = latitude;


        }
    }
}
