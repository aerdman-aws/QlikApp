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
        /// <summary>
        /// Creates a MessageService with a default message provider
        /// </summary>
        public MessageService()
        {
            var providerFactory = new ProviderFactory();
            MessageProvider = providerFactory.GetMessageProvider();
        }

        /// <summary>
        /// Creates a MessageService with a specific message provider.
        /// Can be used for unit testing
        /// </summary>
        /// <param name="messageProvider">The message provider to be used by the service</param>
        public MessageService(IMessageProvider messageProvider)
        {
            MessageProvider = messageProvider;
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
            var message = Get(id); //find the message to remove, by id
            if (message != null)
            {
                MessageProvider.Remove(message); //remove the message
            }

            return message;
        }


        public bool IsPalindrome(Message message)
        {
            if(message == null || String.IsNullOrEmpty(message.Body)) {
                return false;
            }

            var originalMessage = message.Body.Replace(" ", "").ToLower(); //remove white space and convert to lower case
            return originalMessage.Equals(new String(originalMessage.Reverse().ToArray())); //compare the original message to the reversed message
        }
    }
}
