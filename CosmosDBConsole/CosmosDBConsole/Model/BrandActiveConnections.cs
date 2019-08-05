using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosDBConsole.Model
{
    public class BrandActiveConnections
    {
        public string WiserHeat { get; set; }
        public string AuraConnect { get; set; }

        public BrandActiveConnections()
        {

        }

        public BrandActiveConnections(string wiser, string aura)
        {
            WiserHeat = wiser;
            AuraConnect = aura;

        }

    }
}
