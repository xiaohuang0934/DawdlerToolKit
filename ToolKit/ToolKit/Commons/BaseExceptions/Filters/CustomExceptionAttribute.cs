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
                //  做一个统一的处理
                result = new BaseResult(context.Exception.Message);
                context.Result = new ContentResult
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    // 设置返回格式
                    ContentType = "application/json;charset=utf-8",
                    Content = JsonConvert.SerializeObject(result)
                };
                context.ExceptionHandled = true;
            }
            return Task.CompletedTask;
        }
    }
}
