using WPF.Client.Services.Interfaces;

namespace WPF.Client.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}
