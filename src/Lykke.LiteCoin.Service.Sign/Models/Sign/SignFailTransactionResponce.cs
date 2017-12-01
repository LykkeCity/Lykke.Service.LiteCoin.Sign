using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.LiteCoin.Service.Sign.Models.Sign
{

    public enum SignErrorCode
    {
        ValidationError,
        CantSignUsingProvidedPrivateKey
    }

    public class SignFailTransactionResponce
    {
       public SignErrorCode Code { get; set; }
       public string Description { get; set; }
    }
}
