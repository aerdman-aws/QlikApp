using QlikApp.Data.Messages.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QlikApp.Services.Messages
{
    public interface IMessageService
    {
        Message Create(string messageBody);
        Message Get(int id);
        IEnumerable<Message> GetAll();
        Message Remove(int id);
    }
}
