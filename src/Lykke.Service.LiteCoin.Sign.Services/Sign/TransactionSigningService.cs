﻿using System;
using System.Collections.Generic;
using System.Linq;
using Lykke.Service.LiteCoin.Sign.Core.Sign;
using NBitcoin;

namespace Lykke.LiteCoin.Sign.Services.Sign
{
    internal class SignResult : ISignResult
    {
        public string TransactionHex { get; set; }
 

        public static SignResult Ok(string signedHex)
        {
            return new SignResult
            {
                TransactionHex = signedHex
            };
        }
    }

    public class TransactionSigningService: ITransactionSigningService
    {
        private readonly Network _network;

        public TransactionSigningService(Network network)
        {
            _network = network;
        }

        public ISignResult Sign(Transaction tx, IEnumerable<Coin> spentCoins, IReadOnlyCollection<string> privateKeys)
        {
            Key[] secretKeys;

            try
            {
                secretKeys = privateKeys.Select(p => Key.Parse(p, _network)).ToArray();
            }
            catch (FormatException)
            {
                var btcNetwork = Network.Main;

                secretKeys = privateKeys.Select(p => Key.Parse(p, btcNetwork)).ToArray();
            }

            var signed = new TransactionBuilder()
                .AddCoins(spentCoins)
                .AddKeys(secretKeys)
                .SignTransaction(tx);

            return SignResult.Ok(signed.ToHex());
        }
    }
}
