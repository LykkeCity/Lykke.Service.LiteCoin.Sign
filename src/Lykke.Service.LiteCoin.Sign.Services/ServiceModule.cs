using Autofac;
using Common.Log;
using Lykke.Service.LiteCoin.Sign.Core.Settings.ServiceSettings;
using Lykke.Service.LiteCoin.Sign.Core.Sign;
using Lykke.LiteCoin.Sign.Services.Sign;
using Lykke.SettingsReader;
using NBitcoin;

namespace Lykke.LiteCoin.Sign.Services
{
    public  class ServiceModule:Module
    {
        private readonly IReloadingManager<LiteCoinSignSettings> _settings;
        public ServiceModule(IReloadingManager<LiteCoinSignSettings> settings)
        {
            _settings = settings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            RegisterNetwork(builder);
            
            builder.RegisterType<TransactionSigningService>().As<ITransactionSigningService>();
        }

        private void RegisterNetwork(ContainerBuilder builder)
        {
            NBitcoin.Litecoin.Networks.EnsureRegistered();
            builder.RegisterInstance(Network.GetNetwork(_settings.CurrentValue.Network)).As<Network>();
        }
    }
}
