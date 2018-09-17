using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class AuthorizeAttribute : ResultFilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
           // throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string token = context.HttpContext.Request.Headers["token"].ToString();
            AuthorizeService obj = new AuthorizeService();
            bool authorized = obj.IsAuthenticate(token);
            if (authorized != true)
                throw new Exception("UnAuthorized");
        }
    }
}
