using System;
using System.Collections.Generic;
using System.Text;
using Lykke.Service.LiteCoin.Sign.Models;
using Lykke.Service.LiteCoin.Sign.Models.Wallet;
using Microsoft.AspNetCore.Mvc;
using NBitcoin;

namespace Lykke.Service.LiteCoin.Sign.Controllers
{
    [Route("api/[controller]")]
    public class WalletController
    {
        private readonly Network _network;

        public WalletController(Network network)
        {
            _network = network;
        }

        [HttpPost]
        public WalletCreationResponse CreateWallet()
        {
            var key = new Key();

            return new WalletCreationResponse
            {
                PublicAddress = key.PubKey.GetAddress(_network).ToString(),
                PrivateKey = key.GetWif(_network).ToString()
            };
        }
    }
}
