using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Lykke.Service.LiteCoin.Sign.Models.Sign
{
    [DataContract]
    public class SignOkTransactionResponce
    {
        [DataMember(Name = "signedTransaction")]
        public string SignedTransaction { get; set; }
    }
}
