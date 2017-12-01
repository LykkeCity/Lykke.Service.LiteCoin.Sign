﻿namespace Lykke.LiteCoin.Service.Sign.Core.Settings.ServiceSettings
{
    public class SignSettings
    {
        public DbSettings Db { get; set; }

        // ReSharper disable once InconsistentNaming
        public string InsightAPIUrl { get; set; }

        public string Network { get; set; }
        
    }
}
