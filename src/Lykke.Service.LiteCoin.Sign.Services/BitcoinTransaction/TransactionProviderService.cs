using System.Threading.Tasks;
using Common.Log;
using Flurl;
using Flurl.Http;
using Lykke.Service.LiteCoin.Sign.Core.Transaction;
using Lykke.Signing.Services.Helpers;
using NBitcoin;

namespace Lykke.LiteCoin.Sign.Services.BitcoinTransaction
{
    public class TransactionProviderService: ITransactionProviderService
    {
        private readonly InsightsApiSettings _insightsApiSettings;
        private readonly ILog _log;

        public TransactionProviderService(InsightsApiSettings insightsApiSettings, ILog log)
        {
            _insightsApiSettings = insightsApiSettings;
            _log = log;
        }

        public async Task<Transaction> GetTransaction(uint256 hash)
        {
            var resp = await Retry.Try(() => GetTransactionResp(hash),
                ex => ex is FlurlHttpException,
                tryCount: 10,
                logger: _log,
                delayAfterException: 3);

            return Transaction.Parse(resp.RawTx);
        }

        private Task<TransactionInsightsApiResponceContract> GetTransactionResp(uint256 hash)
        {
            return _insightsApiSettings.Url.AppendPathSegment($"insight-lite-api/rawtx/{hash}")
                .GetJsonAsync<TransactionInsightsApiResponceContract>();
        }
    }
}
