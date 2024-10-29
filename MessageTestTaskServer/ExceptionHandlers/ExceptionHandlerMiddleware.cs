namespace MessageTestTaskServer.ExceptionHandlers
{
    // Middleware для обработки исключений в приложении
    public class ExceptionHandlerMiddleware
    {
        // Делегат для следующего компонента в конвейере обработки запросов
        private readonly RequestDelegate _next;

        // Конструктор принимает делегат следующего компонента
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next; // Инициализация поля
        }

        // Асинхронный метод для обработки входящих HTTP-запросов
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Передача управления следующему компоненту в конвейере
                await _next(context);
            }
            catch (CustomException ex)
            {
                // Обработка пользовательского исключения
                context.Response.StatusCode = StatusCodes.Status400BadRequest; // Установка статуса 400 (Некорректный запрос)
                // Запись сообщения об ошибке в ответ
                await context.Response.WriteAsync($"Ошибка: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Обработка всех остальных исключений
                context.Response.StatusCode = StatusCodes.Status500InternalServerError; // Установка статуса 500 (Внутренняя ошибка сервера)
                // Запись сообщения об ошибке в ответ
                await context.Response.WriteAsync($"Внутренняя ошибка сервера: {ex.Message}");
            }
        }
    }
}
