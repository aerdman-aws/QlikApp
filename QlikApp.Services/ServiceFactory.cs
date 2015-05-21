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
        /// <summary>
        /// Returns a reference to a message service that can been used to access the messages on the system.
        /// The factory will determine which implementation of the message service to return
        /// </summary>
        /// <returns>A reference to a message service</returns>
        public IMessageService GetMessageService()
        {
            return new MessageService();
        }
    }
}
