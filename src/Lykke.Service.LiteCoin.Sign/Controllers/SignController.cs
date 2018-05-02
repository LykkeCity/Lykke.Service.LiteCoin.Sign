using System;
using System.Net;
using Lykke.Service.LiteCoin.Sign.Core.Sign;
using Lykke.Service.LiteCoin.Sign.Models;
using Lykke.Service.LiteCoin.Sign.Models.Sign;
using Microsoft.AspNetCore.Mvc;
using NBitcoin;
using NBitcoin.JsonConverters;
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
        public IActionResult SignRawTx([FromBody]SignRequest sourceTx)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ErrorResponse.Create("ValidationError", ModelState));
            }

            (Transaction tx, Coin[] coins) decoded;
            try
            {
                decoded = Serializer.ToObject<(Transaction, Coin[])>(sourceTx.TransactionContext);
            }
            catch(Exception e)
            {
                return BadRequest(ErrorResponse.Create($"Decode transaction context error: {e}"));
            }

            var signResult = _transactionSigningService.Sign(decoded.tx, decoded.coins, sourceTx.PrivateKeys);

            var respResult = new SignOkTransactionResponce
            {
                SignedTransaction = signResult.TransactionHex
            };

            return Ok(respResult);
        }
    }
}
