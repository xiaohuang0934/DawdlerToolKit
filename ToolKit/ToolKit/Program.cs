using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
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
            builder.Services.AddControllers();
            // �滻���������� ====>> �滻��������ڲ���ʵ������ע��
            builder.Services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
            #endregion

            var app = builder.Build();

            #region �ܵ�
            // �жϵ�ǰ���л���
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // ����˶�����·�ɵĿ�����֧��
            app.MapControllers();
            app.UseRouting();
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