using MessageTestTaskServer.Implementation;
using MessageTestTaskServer.Interfaces;
using MessageTestTaskServer.ExceptionHandlers;

namespace MessageTestTaskServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // �������� ������a ��� ��������� ����������
            var builder = WebApplication.CreateBuilder(args);

            // ���������� ����� ��� MVC ������������
            builder.Services.AddControllers();
            // ���������� ������ ��� �������� ��������� ��������
            builder.Services.AddHealthChecks();
            // ����������� IMessageComposer � ��� ���������� MessageComposer � ���������� ������������
            builder.Services.AddScoped<IMessageComposer, MessageComposer>();
            // ���������� ��������� ��� ������������ �������� ����� API
            builder.Services.AddEndpointsApiExplorer();
            // ���������� Swagger ��� ���������������� API
            builder.Services.AddSwaggerGen();

            // ���������� ����������
            var app = builder.Build();

            // ��������, ���������� �� �� � ����� ����������
            if (app.Environment.IsDevelopment())
            {
                // ��������� Swagger � ������ ����������
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // ��������������� HTTP �� HTTPS
            app.UseHttpsRedirection();
            // ������������� �������������� �� ��� ��������� ����������
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            // ������������� �����������
            app.UseAuthorization();
            // ��������� �������� ��� �������� ��������� ��������
            app.MapHealthChecks("/health");
            // ��������� �������� ��� ��������� ��������� �������
            app.MapGet("/health", () => Results.Ok("server_isready"));
            // ��������� ��������� ��� ������������
            app.MapControllers();
            // ������ ����������
            app.Run();
        }
    }
}