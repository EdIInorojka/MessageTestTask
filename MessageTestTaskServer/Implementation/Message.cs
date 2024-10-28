using MessageTestTaskServer.Interfaces;

namespace MessageTestTaskServer.Implementation
{
    public class Message
    {
        private readonly IMessageComposer _messageComposer;

        public Message(IMessageComposer messageComposer)
        {
            _messageComposer = messageComposer;
        }

        public string ReturnMessage()
        {
            DateTime now = DateTime.Now;
            return _messageComposer.ComposeMessage(now);
        }
    }
}
