namespace CosmosDBConsole.Model
{
    public class WiserAuraCount
    {
        public string WiserHeat { get; set; }
        public string AuraConnect { get; set; }

        public WiserAuraCount()
        {

        }

        public WiserAuraCount(string wiser, string aura )
        {
            WiserHeat = wiser;
            AuraConnect = aura;

        }

    }
}
