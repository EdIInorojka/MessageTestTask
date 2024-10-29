using MessageTestTaskServer.Implementation;
using MessageTestTaskServer.Interfaces;
using MessageTestTaskServer.ExceptionHandlers;

namespace MessageTestTaskServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Создание билдерa для настройки приложения
            var builder = WebApplication.CreateBuilder(args);

            // Добавление служб для MVC контроллеров
            builder.Services.AddControllers();
            // Добавление службы для проверки состояния здоровья
            builder.Services.AddHealthChecks();
            // Регистрация IMessageComposer и его реализации MessageComposer в контейнере зависимостей
            builder.Services.AddScoped<IMessageComposer, MessageComposer>();
            // Добавление поддержки для исследования конечных точек API
            builder.Services.AddEndpointsApiExplorer();
            // Добавление Swagger для документирования API
            builder.Services.AddSwaggerGen();

            // Построение приложения
            var app = builder.Build();

            // Проверка, находитесь ли вы в среде разработки
            if (app.Environment.IsDevelopment())
            {
                // Включение Swagger в режиме разработки
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Перенаправление HTTP на HTTPS
            app.UseHttpsRedirection();
            // Использование промежуточного ПО для обработки исключений
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            // Использование авторизации
            app.UseAuthorization();
            // Настройка маршрута для проверки состояния здоровья
            app.MapHealthChecks("/health");
            // Настройка маршрута для получения состояния сервера
            app.MapGet("/health", () => Results.Ok("server_isready"));
            // Настройка маршрутов для контроллеров
            app.MapControllers();
            // Запуск приложения
            app.Run();
        }
    }
}