using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ToolKit.Commons.BaseExceptions.Filters;
using ToolKit.Commons.BaseResults.Filters;
using ToolKit.Configs.AutoFacs;

namespace ToolKit
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region 服务
            // 使用 autofac 的容器工厂替换系统默认的容器
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                // 注入封装类
                containerBuilder.RegisterModule(new AutofacModule());
            });
            // 添加对控制器和与 API 相关的功能，但不是视图或页面的支持
            builder.Services.AddControllers(cnf =>
            {
                // 异常错误过滤
                cnf.Filters.Add<CustomExceptionAttribute>();
                // 结果返回过滤   暂时不用
                //cnf.Filters.Add<ApiResultFilterAttribute>();
            });
            // 替换控制器规则 ====>> 替换后控制器内才能实现属性注入
            builder.Services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
            // swagger服务
            builder.Services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "DawdlerToolKit",
                    Description = "懒人工具箱",
                    Contact = new OpenApiContact { Name = "小新cx330", Email = "18754956388@163.com" }
                });
            });
            #endregion

            var app = builder.Build();

            #region 管道
            // 判断当前运行环境
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // swagger配置中间件
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });
            }
            // 添加了对属性路由的控制器支持
            app.MapControllers();
            app.UseRouting();
            app.UseSwagger();
            
            // 启动页
            app.MapGet("/", () => "欢迎使用工具箱！！！");
            // 添加跨域
            app.UseCors();
            #endregion
            Console.WriteLine("==========工具箱启动！！！==========");
            app.Run();
        }
    }
}