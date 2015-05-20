using QlikApp.Data.Messages.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QlikApp.Data.Messages
{
    public interface IMessageProvider
    {
        Message Create(Message message);
        Message Get(int id);
        IEnumerable<Message> GetAll();
        bool Remove(Message message);
    }
}
