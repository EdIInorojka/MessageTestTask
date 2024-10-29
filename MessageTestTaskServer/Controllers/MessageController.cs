using MessageTestTaskServer.ExceptionHandlers;
using MessageTestTaskServer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MessageTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageComposer _messageComposer;

        public MessageController(IMessageComposer messageComposer)
        {
            _messageComposer = messageComposer;
        }

        [HttpGet(Name = "getMessage")]
        public ActionResult<string> Get()
        {
            try
            {
                var message = _messageComposer.ComposeMessage(DateTime.Now);

                if (string.IsNullOrEmpty(message))
                {
                    throw new CustomException("Сообщение не найдено.");
                }

                return Ok(message);
            }
            catch (ArgumentException argEx)
            {
                throw new CustomException($"Некорректные данные: {argEx.Message}");
            }
        }

        [HttpGet("health")]
        public IActionResult HealthCheck()
        {
            return Ok("server_isready");
        }
    }
}

