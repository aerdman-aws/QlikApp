using QlikApp.Data;
using QlikApp.Data.Messages;
using QlikApp.Data.Messages.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QlikApp.Services.Messages
{
    public class MessageService : IMessageService
    {
        public MessageService()
        {
            var providerFactory = new ProviderFactory();
            MessageProvider = providerFactory.GetMessageProvider();
        }

        protected IMessageProvider MessageProvider { get; set; }

        public Message Create(string messageBody)
        {
            var message = new Message
            {
                Body = messageBody
            };

            return MessageProvider.Create(message);
        }

        public Message Get(int id)
        {
            return MessageProvider.Get(id);
        }

        public IEnumerable<Message> GetAll()
        {
            return MessageProvider.GetAll();
        }

        public Message Remove(int id)
        {
            var message = Get(id);
            if (message != null)
            {
                MessageProvider.Remove(message);
            }

            return message;
        }
    }
}
