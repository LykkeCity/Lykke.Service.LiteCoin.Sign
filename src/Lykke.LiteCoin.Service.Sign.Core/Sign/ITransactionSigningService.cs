using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.LiteCoin.Service.Sign.Core.Sign
{
    public interface ISignResult
    {
        string TransactionHex { get; }

        bool IsSuccess { get; }

        SignError? Error { get; }
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
