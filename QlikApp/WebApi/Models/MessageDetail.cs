using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QlikApp.Web.WebApi.Models
{
    /// <summary>
    /// A message in the system and additional information about it
    /// </summary>
    public class MessageDetail
    {
        public MessageDetail()
        {
        }

        public MessageDetail(Message message)
        {
            this.Message = message;
        }

        /// <summary>
        /// The message
        /// </summary>
        public Message Message { get; set; }

        /// <summary>
        /// Whether the message text is a palindrome
        /// </summary>
        public bool IsPalindrome { get; set; }
    }
}