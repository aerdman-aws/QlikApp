using QlikApp.Data.Messages.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QlikApp.Data.Messages.Providers
{
    public class InMemoryProvider : IMessageProvider
    {
        private static int ID = 1;
        private static List<Message> messages = new List<Message>(new[]
        { 
            new Message { Id = ID++, Body = "Hello world" },
            new Message { Id = ID++, Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." },
            new Message { Id = ID++, Body = "Never odd or even" },
        });

        public Message Create(Message message)
        {
            if (message == null)
            {
                return null;
            }

            //generate a unique id for the message
            message.Id = ID++;

            messages.Add(message);
            return message;
        }

        public Message Get(int id)
        {
            return messages.FirstOrDefault(m => m.Id == id);
        }

        public IEnumerable<Message> GetAll()
        {
            return messages;
        }

        public bool Remove(Message message)
        {
            if (message == null)
            {
                return false;
            }

            return messages.Remove(message);
        }
    }
}
