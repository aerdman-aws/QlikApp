using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QlikApp.Web.WebApi.Models
{
    /// <summary>
    /// A message in the system
    /// </summary>
    public class Message
    {
        /// <summary>
        /// A unique identifier for the message
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The actual message text
        /// </summary>
        public string Body { get; set; }
    }
}