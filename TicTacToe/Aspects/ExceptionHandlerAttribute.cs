using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Model;

namespace TicTacToe.Aspects
{
    public class ExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        Logger1 logObject = new Logger1();
        Logservice logserviceobject = new Logservice();

        public override void OnException(ExceptionContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is Exception)
            {
                logObject.Request = actionExecutedContext.RouteData.Values["controller"].ToString() + " " + actionExecutedContext.RouteData.Values["action"].ToString();
                logObject.Exception = actionExecutedContext.Exception.ToString();
                var index = logObject.Exception.IndexOf("\n");
                logObject.Exception = logObject.Exception.Substring(0, index);
                logObject.Response = "RequestFailure";
                logserviceobject.Add(logObject);
            }
        }
    }
}
