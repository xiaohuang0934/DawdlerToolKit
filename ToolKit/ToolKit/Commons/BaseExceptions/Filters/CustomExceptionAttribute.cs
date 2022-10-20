using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ToolKit.Commons.BaseResults;
using Newtonsoft.Json;

namespace ToolKit.Commons.BaseExceptions.Filters
{
    public class CustomExceptionAttribute : IAsyncExceptionFilter
    {
        /// <summary>
        /// 重写异常出现处理逻辑
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task OnExceptionAsync(ExceptionContext context)
        {
            // 如果异常没有被处理则进行处理
            if (context.ExceptionHandled)
            {
                // TODO 如果异常没有被处理则进行处理
            }
            BaseResult result = null;
            if (context.Exception is BaseException)
            {
                BaseException baseException = (BaseException)context.Exception;
                result = new BaseResult(baseException.Message);
                context.Result = new ContentResult
                {
                    // 返回状态码设置为200，表示成功 TODO状态码改为200
                    StatusCode = StatusCodes.Status500InternalServerError,
                    // 设置返回格式
                    ContentType = "application/json;charset=utf-8",
                    Content = JsonConvert.SerializeObject(result)
                };

                // 设置为true，表示异常已经被处理
                context.ExceptionHandled = true;
            }
            else
            {
                // TODO 做一个统一的处理
            }
            return Task.CompletedTask;
        }
    }
}
