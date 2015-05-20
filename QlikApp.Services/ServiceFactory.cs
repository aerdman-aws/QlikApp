using QlikApp.Services.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QlikApp.Services
{
    public class ServiceFactory
    {
        public IMessageService GetMessageService()
        {
            return new MessageService();
        }
    }
}
