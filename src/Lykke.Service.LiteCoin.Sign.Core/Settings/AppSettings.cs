using Lykke.LiteCoin.Core.Settings.SlackNotifications;
using Lykke.LiteCoin.Service.Sign.Core.Settings.ServiceSettings;

namespace Lykke.LiteCoin.Service.Sign.Core.Settings
{
    public class AppSettings
    {
        public SignSettings SignSettings { get; set; }
        public SlackNotificationsSettings SlackNotifications { get; set; }
    }
}
