using Autofac;
using Common.Log;
using Lykke.LiteCoin.Service.Sign.Core.Settings.ServiceSettings;
using Lykke.LiteCoin.Service.Sign.Core.Sign;
using Lykke.LiteCoin.Service.Sign.Core.Transaction;
using Lykke.LiteCoin.Sign.Services.BitcoinTransaction;
using Lykke.LiteCoin.Sign.Services.Sign;
using Lykke.SettingsReader;
using NBitcoin;

namespace Lykke.LiteCoin.Sign.Services
{
    public  class ServiceModule:Module
    {
        private readonly ILog _log;
        private readonly IReloadingManager<SignSettings> _settings;
        public ServiceModule(IReloadingManager<SignSettings> settings, ILog log)
        {
            _log = log;
            _settings = settings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            RegisterNetwork(builder);

            builder.RegisterType<TransactionProviderService>().As<ITransactionProviderService>();
            builder.RegisterType<TransactionSigningService>().As<ITransactionSigningService>();
        }

        private void RegisterNetwork(ContainerBuilder builder)
        {
            NBitcoin.Litecoin.Networks.Register();
            builder.RegisterInstance(Network.GetNetwork(_settings.CurrentValue.Network)).As<Network>();

            builder.RegisterInstance(new InsightsApiSettings
            {
                Url = _settings.CurrentValue.InsightAPIUrl
            });
        }
    }
}
