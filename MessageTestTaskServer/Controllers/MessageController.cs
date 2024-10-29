using MessageTestTaskServer.ExceptionHandlers;
using MessageTestTaskServer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MessageTask.Controllers
{
    // Указывает, что данный класс является контроллером API
    [ApiController]
    // Определяет маршрут для контроллера
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        // Поле для хранения экземпляра IMessageComposer
        private readonly IMessageComposer _messageComposer;

        // Конструктор для внедрения зависимости IMessageComposer
        public MessageController(IMessageComposer messageComposer)
        {
            _messageComposer = messageComposer; // Инициализация поля
        }

        // Обработчик GET-запроса для получения сообщения
        [HttpGet(Name = "getMessage")]
        public ActionResult<string> Get()
        {
            try
            {
                // Композирование сообщения с использованием текущей даты и времени
                var message = _messageComposer.ComposeMessage(DateTime.Now);

                // Проверка на наличие сообщения
                if (string.IsNullOrEmpty(message))
                {
                    // Если сообщение пустое, выбрасывается пользовательское исключение
                    throw new CustomException("Сообщение не найдено.");
                }

                // Возвращение успешного ответа с сообщением
                return Ok(message);
            }
            catch (ArgumentException argEx)
            {
                // Обработка исключений ArgumentException и выбрасывание пользовательского исключения с сообщением
                throw new CustomException($"Некорректные данные: {argEx.Message}");
            }
        }

        // Обработчик GET-запроса для проверки состояния сервера
        [HttpGet("health")]
        public IActionResult HealthCheck()
        {
            // Возвращение успешного ответа, если сервер готов
            return Ok("server_isready");
        }
    }
}
