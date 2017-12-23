using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.LiteCoin.Sign.Core.Exceptions
{
    public class BackendException : Exception
    {
        public ErrorCode Code { get; private set; }
        public string Text { get; private set; }

        public BackendException(string text, ErrorCode code)
            : base(text)
        {
            Code = code;
            Text = text;
        }
    }

    public enum ErrorCode
    {
        IncompatiblePrivateKey,

        InvalidScript

    }
}
