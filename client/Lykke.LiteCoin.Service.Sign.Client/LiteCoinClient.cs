using System;
using Common.Log;

namespace Lykke.LiteCoin.Service.Sign.Client
{
    public class LiteCoinClient : ILiteCoinClient, IDisposable
    {
        private readonly ILog _log;

        public LiteCoinClient(string serviceUrl, ILog log)
        {
            _log = log;
        }

        public void Dispose()
        {
            //if (_service == null)
            //    return;
            //_service.Dispose();
            //_service = null;
        }
    }
}
