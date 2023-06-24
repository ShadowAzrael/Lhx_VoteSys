using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lhx_VoteSys.Filter
{
    public class GlobalActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //LogHelper.Info("OnActionExecuted");
            //执行方法后执行这
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //LogHelper.Info("OnActionExecuting");
            //执行方法前先执行这
            foreach (var item in context.HttpContext.Request.Query)
            {
                var k = item.Key;
                var v = item.Value;
                if (item.Value.ToString().Contains(""))
                {
                    context.Result = new JsonResult(new
                    {
                        status = false,
                        msg = "message error:"
                    });
                }
            }

        }
    }
}
