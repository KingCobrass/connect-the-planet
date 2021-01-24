using Connect.Interface.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect.API.Infrastructure
{
    /// <summary>
    /// Request validator
    /// </summary>
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Action Execution
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (actionContext.ActionArguments.Any(kv => kv.Value == null))
            {
                actionContext.Result = new BadRequestObjectResult(ConnectConstants.REQUIRED_PARAMETER_NOT_EMPTY);
                return;
            }

            if (!actionContext.ModelState.IsValid)
            {
                var message = string.Join(" | ", actionContext.ModelState.Values
                                  .SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage));
                if (string.IsNullOrEmpty(message)) message = ConnectConstants.REQUIRED_PARAMETER_NOT_EMPTY;

                actionContext.Result = new BadRequestObjectResult(message);
            }

        }
    }
}
