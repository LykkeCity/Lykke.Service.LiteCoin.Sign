using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Lykke.LiteCoin.Service.Sign.Core.Sign;
using Lykke.LiteCoin.Service.Sign.Core.Transaction;
using Lykke.LiteCoin.Service.Sign.Helpers;
using Lykke.LiteCoin.Service.Sign.Models.Sign;
using Lykke.LiteCoin.Service.Sign.Service.Sign;
using Lykke.LiteCoin.Service.Sign.Service.Sign.Models;
using Microsoft.AspNetCore.Mvc;
using NBitcoin;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lykke.LiteCoin.Service.Sign.Controllers
{
    [Route("api/[controller]")]
    public class SignController : Controller
    {
        private readonly ITransactionSigningService _transactionSigningService;

        public SignController(ITransactionSigningService transactionSigningService)
        {
            _transactionSigningService = transactionSigningService;
        }
        
        [HttpPost]
        [SwaggerOperation(nameof(SignRawTx))]
        [ProducesResponseType(typeof(SignOkTransactionResponce), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SignFailTransactionResponce), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SignRawTx([FromBody]SignTransactionRequest sourceTx)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new SignFailTransactionResponce
                {
                    Code = SignErrorCode.ValidationError,
                    Description = ModelState.GetErrorsString()
                });
            }

            var signResult = await _transactionSigningService.SignAsync(sourceTx.Transaction, sourceTx.PrivateKeys);

            if (signResult.IsSuccess)
            {
                var respResult = new SignOkTransactionResponce
                {
                    Transaction = signResult.TransactionHex
                };

                return Ok(respResult);
            }

            return BadRequest(new SignFailTransactionResponce
            {
                Code = SignErrorCode.CantSignUsingProvidedPrivateKey
            });
        }
    }
}
