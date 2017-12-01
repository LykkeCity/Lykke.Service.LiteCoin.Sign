using System;
using System.Collections.Generic;
using System.Text;
using Lykke.Service.LiteCoin.Sign.Core;
using NBitcoin;

namespace Lykke.LiteCoin.Sign.Services.Wallet
{
    internal class GeneratedWallet : IGeneratedWallet
    {
        public string Address { get; set; }
        public string PrivateKey { get; set; }
    }

    public class WalletGenerator: IWalletGenerator
    {
        private readonly Network _network;

        public WalletGenerator(Network network)
        {
            _network = network;
        }

        public IGeneratedWallet Generate()
        {
            var key = new Key();

            return new GeneratedWallet
            {
                Address = key.PubKey.GetAddress(_network).ToString(),
                PrivateKey = key.GetWif(_network).ToString()
            };      
        }
    }
}
