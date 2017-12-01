using Lykke.LiteCoin.Core.Settings.SlackNotifications;
using Lykke.Service.LiteCoin.Sign.Core.Settings.ServiceSettings;

namespace Lykke.Service.LiteCoin.Sign.Core.Settings
{
    public class AppSettings
    {
        public LiteCoinSignSettings LiteCoinSign { get; set; }
        public SlackNotificationsSettings SlackNotifications { get; set; }
    }
}
