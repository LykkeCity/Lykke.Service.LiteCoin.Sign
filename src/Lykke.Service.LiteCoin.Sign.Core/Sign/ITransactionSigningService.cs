using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NBitcoin;

namespace Lykke.Service.LiteCoin.Sign.Core.Sign
{
    public interface ISignResult
    {
        string TransactionHex { get; }
    }

    public enum SignError
    {
        IncompatiblePrivateKey,

        InvalidScript
    }

    public interface ITransactionSigningService
    {
        ISignResult Sign(Transaction tx, IEnumerable<Coin> spentCoins, IReadOnlyCollection<string> privateKeys);
    }
}
