using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.LiteCoin.Sign.Tests
{
    public static class PrepareNetworks
    {
        static PrepareNetworks()
        {
            NBitcoin.Litecoin.Networks.Register();
        }

        public static void EnsureLiteCoinPrepared()
        {
            
        }
    }
}
