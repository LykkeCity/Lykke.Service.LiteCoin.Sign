using System;
using System.Collections.Generic;
using System.Text;
using Lykke.LiteCoin.Sign.Services.Wallet;
using NBitcoin;
using Xunit;

namespace Lykke.Service.LiteCoin.Sign.Tests
{
    
    public class PrivateKeyTests
    {
        
        [Fact]
        public void CanGenerateValidPrivateKey()
        {
            PrepareNetworks.EnsureLiteCoinPrepared();

            var network = Network.TestNet;

            var generator = new WalletGenerator(network);

            var generatedWallet = generator.Generate();

            var key = Key.Parse(generatedWallet.PrivateKey, network);
            var addressFromKey = key.PubKey.GetAddress(network).ToString();


            var btcAddress = BitcoinAddress.Create(generatedWallet.Address, network);

            Assert.True(generatedWallet.Address == addressFromKey);
            Assert.True(generatedWallet.Address == btcAddress.ToString());
            Assert.True(generatedWallet.PrivateKey == key.ToString(network));
        }
    }
}
