using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QlikApp.Data.Messages.Model
{
    public class Message
    {
        /// <summary>
        /// The unique message identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The actual text of the message
        /// </summary>
        public string Body { get; set; }
    }
}
