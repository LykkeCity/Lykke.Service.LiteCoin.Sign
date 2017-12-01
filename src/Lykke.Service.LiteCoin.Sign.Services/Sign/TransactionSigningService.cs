using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Log;
using Lykke.LiteCoin.Service.Sign.Core.Sign;
using Lykke.LiteCoin.Service.Sign.Core.Transaction;
using NBitcoin;

namespace Lykke.LiteCoin.Sign.Services.Sign
{
    internal class SignResult : ISignResult
    {
        public string TransactionHex { get; set; }
        public bool IsSuccess { get; set; }
        public SignError? Error { get; set; }

        public static SignResult Ok(string signedHex)
        {
            return new SignResult
            {
                IsSuccess = true,
                TransactionHex = signedHex
            };
        }

        public static SignResult Fail(SignError error)
        {
            return new SignResult
            {
                IsSuccess = false,
                Error = error
            };
        }
    }

    public class TransactionSigningService: ITransactionSigningService
    {
        private readonly Network _network;
        private readonly ILog _log;
        private readonly ITransactionProviderService _transactionProviderService;

        public TransactionSigningService(Network network, ILog log, ITransactionProviderService transactionProviderService)
        {
            _network = network;
            _log = log;
            _transactionProviderService = transactionProviderService;
        }

        public async Task<ISignResult> SignAsync(string transactionHex, IEnumerable<string> privateKeys)
        {
            var tx = new Transaction(transactionHex);

            var secretKeys = privateKeys.Select(p=>Key.Parse(p, _network)).ToList();

            Key GetPrivateKey(KeyId pubKeyHash)
            {
                foreach (var secret in secretKeys)
                {
                    var key = new BitcoinSecret(secret, _network);
                    if (key.PubKey.Hash == pubKeyHash)
                        return key.PrivateKey;
                }

                return null;
            }

            SigHash hashType = SigHash.All;

            for (int i = 0; i < tx.Inputs.Count; i++)
            {
                var input = tx.Inputs[i];

                var prevTransaction = await _transactionProviderService.GetTransaction(input.PrevOut.Hash);
                
                var output = prevTransaction.Outputs[input.PrevOut.N];

                if (PayToPubkeyHashTemplate.Instance.CheckScriptPubKey(output.ScriptPubKey))
                {
                    var secret = GetPrivateKey(PayToPubkeyHashTemplate.Instance.ExtractScriptPubKeyParameters(output.ScriptPubKey));
                    if (secret != null)
                    {
                        var hash = Script.SignatureHash(output.ScriptPubKey, tx, i, hashType);
                        var signature = secret.Sign(hash, hashType);

                        tx.Inputs[i].ScriptSig = PayToPubkeyHashTemplate.Instance.GenerateScriptSig(signature, secret.PubKey);

                        continue;
                    }

                    return SignResult.Fail(SignError.IncompatiblePrivateKey);
                }

                if (PayToPubkeyTemplate.Instance.CheckScriptPubKey(output.ScriptPubKey))
                {
                    var secret = GetPrivateKey(PayToPubkeyTemplate.Instance.ExtractScriptPubKeyParameters(output.ScriptPubKey).Hash);
                    if (secret != null)
                    {
                        var hash = Script.SignatureHash(output.ScriptPubKey, tx, i, hashType);
                        var signature = secret.Sign(hash, hashType);

                        tx.Inputs[i].ScriptSig = PayToPubkeyTemplate.Instance.GenerateScriptSig(signature);

                        continue;
                    }

                    return SignResult.Fail(SignError.IncompatiblePrivateKey);
                }


                return SignResult.Fail(SignError.InvalidScript);
            }

            return SignResult.Ok(tx.ToHex());
        }
    }
}
