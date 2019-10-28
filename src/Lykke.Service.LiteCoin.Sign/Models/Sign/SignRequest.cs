using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace Lykke.Service.LiteCoin.Sign.Models.Sign
{
    [DataContract]
    public class SignRequest:IValidatableObject
    {
        [DataMember(Name = "transactionContext")]
        public string TransactionContext { get; set; }

        [DataMember(Name = "privateKeys")]
        public IReadOnlyCollection<string> PrivateKeys { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
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
