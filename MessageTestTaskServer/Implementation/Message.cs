using MessageTestTaskServer.Interfaces;

namespace MessageTestTaskServer.Implementation
{
    // Класс Message для обработки сообщений
    public class Message
    {
        // Поле для хранения экземпляра IMessageComposer
        private readonly IMessageComposer _messageComposer;

        // Конструктор принимает IMessageComposer и инициализирует поле
        public Message(IMessageComposer messageComposer)
        {
            _messageComposer = messageComposer; // Инициализация поля с помощью внедрения зависимости
        }

        // Метод для возвращения скомпонованного сообщения
        public string ReturnMessage()
        {
            DateTime now = DateTime.Now; // Получение текущей даты и времени
            // Вызов метода ComposeMessage у IMessageComposer для получения сообщения
            return _messageComposer.ComposeMessage(now);
        }
    }
}
