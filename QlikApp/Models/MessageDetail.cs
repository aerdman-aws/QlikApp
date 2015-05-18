using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QlikApp.Models
{
    public class MessageDetail
    {
        public MessageDetail()
        {
        }

        public MessageDetail(Message message)
        {
            this.Message = message;
            this.IsPalindrome = this.isPalindrome(message.Body);
        }

        public Message Message { get; set; }
        public bool IsPalindrome { get; set; }

        private bool isPalindrome(string text) //TODO: pull out of data class
        {
            return text.Equals(new String(text.Reverse().ToArray()));
        }
    }
}