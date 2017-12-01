using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.LiteCoin.Service.Sign.Models
{
    public class GenerateWalletResponce
    {
        public string PrivateKey { get; set; }

        public string Address { get; set; }
    }
}
