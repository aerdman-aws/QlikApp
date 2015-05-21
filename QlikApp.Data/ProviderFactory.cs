using QlikApp.Data.Messages;
using QlikApp.Data.Messages.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QlikApp.Data
{
    public class ProviderFactory
    {
        private static IMessageProvider MessageProvider { get; set; } //temporary implementation detail: keeping as static to retain in memory collection

        /// <summary>
        /// Returns a reference to a message provider that can been used to access the messages on the system.
        /// The factory will determine which implementation of the message provider to return
        /// </summary>
        /// <returns>A reference to a message provider</returns>
        public IMessageProvider GetMessageProvider()
        {
            if (MessageProvider == null)
            {
                MessageProvider = new InMemoryProvider();
            }
            return MessageProvider;
        }
    }
}
