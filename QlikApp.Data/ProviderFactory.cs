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
        private static IMessageProvider MessageProvider { get; set; } //TODO: keeping as static to retain in memory collection

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
