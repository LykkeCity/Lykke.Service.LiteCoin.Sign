using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.LiteCoin.Sign.Core
{
    public interface IGeneratedWallet
    {
        string Address { get; }
        string PrivateKey { get; }
    }
    public interface IWalletGenerator
    {
        IGeneratedWallet Generate();
    }
}
