using System.Collections.Generic;
using System.Linq;
using Lykke.Service.LiteCoin.Sign.Core.Exceptions;
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

        public ISignResult Sign(Transaction tx, IEnumerable<ICoin> spentCoins, IEnumerable<string> privateKeys)
        {
            var secretKeys = privateKeys.Select(p=>Key.Parse(p, _network)).ToList();

            Key GetPrivateKey(TxDestination pubKeyHash)
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
                
                var output = spentCoins.Single(p=>p.Outpoint==input.PrevOut);

                if (PayToPubkeyHashTemplate.Instance.CheckScriptPubKey(output.GetScriptCode()))
                {
                    var secret = GetPrivateKey(PayToPubkeyHashTemplate.Instance.ExtractScriptPubKeyParameters(output.GetScriptCode()));
                    if (secret != null)
                    {
                        var hash = Script.SignatureHash(output.GetScriptCode(), tx, i, hashType);
                        var signature = secret.Sign(hash, hashType);

                        tx.Inputs[i].ScriptSig = PayToPubkeyHashTemplate.Instance.GenerateScriptSig(signature, secret.PubKey);

                        continue;
                    }

                    throw new BusinessException("Incompatible private key", ErrorCode.IncompatiblePrivateKey);
                }

                if (PayToPubkeyTemplate.Instance.CheckScriptPubKey(output.GetScriptCode()))
                {
                    var secret = GetPrivateKey(PayToPubkeyTemplate.Instance.ExtractScriptPubKeyParameters(output.GetScriptCode()).Hash);
                    if (secret != null)
                    {
                        var hash = Script.SignatureHash(output.GetScriptCode(), tx, i, hashType);
                        var signature = secret.Sign(hash, hashType);

                        tx.Inputs[i].ScriptSig = PayToPubkeyTemplate.Instance.GenerateScriptSig(signature);

                        continue;
                    }

                    throw new BusinessException("Incompatible private key", ErrorCode.IncompatiblePrivateKey);
                }


                throw new BusinessException("Incompatible private key", ErrorCode.InvalidScript);
            }

            return SignResult.Ok(tx.ToHex());
        }
    }
}
