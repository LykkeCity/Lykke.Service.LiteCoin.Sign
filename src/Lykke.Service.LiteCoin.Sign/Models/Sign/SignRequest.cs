using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Lykke.Service.LiteCoin.Sign.Models.Sign
{
    public class SignRequest:IValidatableObject
    {
        public string TransactionHex { get; set; }

        public IEnumerable<string> PrivateKeys { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            try
            {

                NBitcoin.Transaction.Parse(TransactionHex);
            }
            catch 
            {
                return new[]
                {
                    new ValidationResult("Cant parse tx", new[] {nameof(TransactionHex)}),
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
