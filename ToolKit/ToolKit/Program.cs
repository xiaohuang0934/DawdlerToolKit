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

            #region ����
            // ʹ�� autofac �����������滻ϵͳĬ�ϵ�����
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                // ע���װ��
                containerBuilder.RegisterModule(new AutofacModule());
            });
            // ��ӶԿ��������� API ��صĹ��ܣ���������ͼ��ҳ���֧��
            builder.Services.AddControllers(cnf =>
            {
                // �쳣�������
                cnf.Filters.Add<CustomExceptionAttribute>();
                // ������ع���   ��ʱ����
                //cnf.Filters.Add<ApiResultFilterAttribute>();
            });
            // �滻���������� ====>> �滻��������ڲ���ʵ������ע��
            builder.Services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
            // swagger����
            builder.Services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "DawdlerToolKit",
                    Description = "���˹�����",
                    Contact = new OpenApiContact { Name = "С��cx330", Email = "18754956388@163.com" }
                });
            });
            #endregion

            var app = builder.Build();

            #region �ܵ�
            // �жϵ�ǰ���л���
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // swagger�����м��
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });
            }
            // ����˶�����·�ɵĿ�����֧��
            app.MapControllers();
            app.UseRouting();
            app.UseSwagger();
            
            // ����ҳ
            app.MapGet("/", () => "��ӭʹ�ù����䣡����");
            // ��ӿ���
            app.UseCors();
            #endregion
            Console.WriteLine("==========����������������==========");
            app.Run();
        }
    }
}