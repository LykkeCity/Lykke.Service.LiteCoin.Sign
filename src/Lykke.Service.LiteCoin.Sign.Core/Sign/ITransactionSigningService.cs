using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<ISignResult> SignAsync(string transactionHex, IEnumerable<string> privateKeys);
    }
}
