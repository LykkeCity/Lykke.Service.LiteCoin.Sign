using System.Threading.Tasks;
using NBitcoin;

namespace Lykke.Service.LiteCoin.Sign.Core.Transaction
{
    public interface ITransactionProviderService
    {
        Task<NBitcoin.Transaction> GetTransaction(uint256 hash);
    }
}
