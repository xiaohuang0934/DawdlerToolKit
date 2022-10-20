using ToolKit.Commons.BaseExceptions;

namespace ToolKit.Services.Test
{
    public class TestService : ITestService
    {
        public async Task<string> QueryString()
        {
            string result = string.Empty;
            await Task.Run(() =>
              {
                  result = "这是一个异步方法";
                  //throw new BaseException("异步异常抛出");
              });
            //throw new BaseException("同步异常抛出");
            return result;
        }
    }
}
