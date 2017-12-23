using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Lykke.Service.LiteCoin.Sign.Core.Sign;
using Lykke.Service.LiteCoin.Sign.Core.Transaction;
using Lykke.Service.LiteCoin.Sign.Helpers;
using Lykke.Service.LiteCoin.Sign.Models;
using Lykke.Service.LiteCoin.Sign.Models.Sign;
using Lykke.Service.LiteCoin.Sign.Service.Sign;
using Lykke.Service.LiteCoin.Sign.Service.Sign.Models;
using Microsoft.AspNetCore.Mvc;
using NBitcoin;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lykke.Service.LiteCoin.Sign.Controllers
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
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SignRawTx([FromBody]SignRequest sourceTx)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ErrorResponse.Create("ValidationError", ModelState));
            }

            var signResult = await _transactionSigningService.SignAsync(sourceTx.TransactionHex, sourceTx.PrivateKeys);

            var respResult = new SignOkTransactionResponce
            {
                SignedTransaction = signResult.TransactionHex
            };

            return Ok(respResult);
        }
    }
}
