namespace ToolKit
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region 添加服务
            // 添加对控制器和与 API 相关的功能，但不是视图或页面的支持
            builder.Services.AddControllers();
            #endregion

            var app = builder.Build();

            #region 配置管道
            // 判断当前运行环境
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // 添加了对属性路由的控制器支持
            app.MapControllers();

            app.UseRouting();
            // 添加跨域
            app.UseCors();

            #endregion

            app.Run();
        }
    }
}