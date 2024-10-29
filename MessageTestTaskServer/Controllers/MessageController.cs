using MessageTestTaskServer.ExceptionHandlers;
using MessageTestTaskServer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MessageTask.Controllers
{
    // ���������, ��� ������ ����� �������� ������������ API
    [ApiController]
    // ���������� ������� ��� �����������
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        // ���� ��� �������� ���������� IMessageComposer
        private readonly IMessageComposer _messageComposer;

        // ����������� ��� ��������� ����������� IMessageComposer
        public MessageController(IMessageComposer messageComposer)
        {
            _messageComposer = messageComposer; // ������������� ����
        }

        // ���������� GET-������� ��� ��������� ���������
        [HttpGet(Name = "getMessage")]
        public ActionResult<string> Get()
        {
            try
            {
                // �������������� ��������� � �������������� ������� ���� � �������
                var message = _messageComposer.ComposeMessage(DateTime.Now);

                // �������� �� ������� ���������
                if (string.IsNullOrEmpty(message))
                {
                    // ���� ��������� ������, ������������� ���������������� ����������
                    throw new CustomException("��������� �� �������.");
                }

                // ����������� ��������� ������ � ����������
                return Ok(message);
            }
            catch (ArgumentException argEx)
            {
                // ��������� ���������� ArgumentException � ������������ ����������������� ���������� � ����������
                throw new CustomException($"������������ ������: {argEx.Message}");
            }
        }

        // ���������� GET-������� ��� �������� ��������� �������
        [HttpGet("health")]
        public IActionResult HealthCheck()
        {
            // ����������� ��������� ������, ���� ������ �����
            return Ok("server_isready");
        }
    }
}
