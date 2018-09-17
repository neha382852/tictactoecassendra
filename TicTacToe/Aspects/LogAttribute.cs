using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Model;

namespace TicTacToe.Aspects
{
    public class LogAttribute : ResultFilterAttribute, IActionFilter
    {
        Logger1 logObject = new Logger1();
        Logservice logserviceObject = new Logservice();
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null)
            {
                logObject.Request = context.RouteData.Values["controller"].ToString() + " " + context.RouteData.Values["action"].ToString()  ;
                logObject.Response = "Success";
                logObject.Exception = "NONE";
                logserviceObject.Add(logObject);
            }

        }

        public void OnActionExecuting(ActionExecutingContext context)
        { 

        }
    }
}
