using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Lykke.LiteCoin.Service.Sign.Helpers
{
    public static class ModelStateHelper
    {
        public static List<string> GetErrorsList(this ModelStateDictionary modelState)
        {
            var query = from state in modelState.Values
                from error in state.Errors
                select error.ErrorMessage;

            var errorList = query.ToList();
            return errorList;
        }

        public static string GetErrorsString(this ModelStateDictionary modelState)
        {
            var errorList = GetErrorsList(modelState);

            return string.Join(", ", errorList);
        }
    }
}
