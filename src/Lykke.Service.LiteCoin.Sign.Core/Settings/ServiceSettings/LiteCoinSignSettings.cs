namespace Lykke.Service.LiteCoin.Sign.Core.Settings.ServiceSettings
{
    public class LiteCoinSignSettings
    {
        public DbSettings Db { get; set; }

        // ReSharper disable once InconsistentNaming
        public string InsightAPIUrl { get; set; }

        public string Network { get; set; }
        
    }
}
