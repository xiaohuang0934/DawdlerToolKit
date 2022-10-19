namespace ToolKit
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region ��ӷ���
            // ��ӶԿ��������� API ��صĹ��ܣ���������ͼ��ҳ���֧��
            builder.Services.AddControllers();
            #endregion

            var app = builder.Build();

            #region ���ùܵ�
            // �жϵ�ǰ���л���
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // ����˶�����·�ɵĿ�����֧��
            app.MapControllers();

            app.UseRouting();
            // ��ӿ���
            app.UseCors();

            #endregion

            app.Run();
        }
    }
}