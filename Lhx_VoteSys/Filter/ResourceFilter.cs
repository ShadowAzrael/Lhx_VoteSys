using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lhx_VoteSys.Filter
{
    /// <summary>
    /// 进行资源缓存和防盗链
    /// </summary>
    public class ResourceFilter : IResourceFilter
    {
        /// <param name="context"></param>
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            // 执行完后的操作
        }
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            // 执行中的过滤器管道
        }
    }
}
