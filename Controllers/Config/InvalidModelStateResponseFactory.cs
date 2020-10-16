using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Budget.API.Extensions;
using Budget.API.Resources;

namespace Budget.API.Controllers.Config
{
    public static class InvalidModelStateResponseFactory
    {
        public static IActionResult ProduceErrorResponse(ActionContext context)
        {
            var errors = context.ModelState.GetErrorMessages();
            var response = new ErrorResource(messages: errors);
            
            return new BadRequestObjectResult(response);
        }
    }
}