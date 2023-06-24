using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lhx_VoteSys.Filter
{
    public class AuthorizeFilter : IAuthorizationFilter
    {
        /// <summary>
        /// 实现验证权限的方法
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //这里可以做复杂的权限控制操作
            //if (context.HttpContext.User.Identity.Name != "1") //简单的做一个示范
            //{
            // //未通过验证则跳转到无权限提示页
            // RedirectToActionResult content = new RedirectToActionResult("NoAuth", "Exception", null);
            // context.Result = content;
            //
        }
    }
}
