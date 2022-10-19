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
              });
            return result;
        }
    }
}
