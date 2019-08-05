using System;
using Microsoft.Extensions.Configuration;

namespace DataRepository
{
    public class AppSettings
    {
        public string StorageConnectionString { get; set; }

        public string EcoIQConnectionString { get; set; }
        public static AppSettings LoadAppSettings()
        {
            IConfigurationRoot configRoot = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("Settings.json")
                .Build();
            AppSettings appSettings = configRoot.Get<AppSettings>();
            return appSettings;
        }

    }
}
