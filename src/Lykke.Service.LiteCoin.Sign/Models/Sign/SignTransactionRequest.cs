using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Lykke.LiteCoin.Service.Sign.Models.Sign
{
    public class SignTransactionRequest:IValidatableObject
    {
        public string Transaction { get; set; }

        public IEnumerable<string> PrivateKeys { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            try
            {

                NBitcoin.Transaction.Parse(Transaction);
            }
            catch 
            {
                return new[]
                {
                    new ValidationResult("Cant parse tx", new[] {nameof(Transaction)}),
                };
            }

            try
            {
                foreach (var privateKey in PrivateKeys)
                {
                    NBitcoin.Key.Parse(privateKey);
                }
            }
            catch
            {
                return new[]
                {
                    new ValidationResult("Cant parse privateKey", new[] {nameof(PrivateKeys) }),
                };
            }

            return Enumerable.Empty<ValidationResult>();
        }
    }
}
