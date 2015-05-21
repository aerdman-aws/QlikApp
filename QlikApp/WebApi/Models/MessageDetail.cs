using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QlikApp.Web.WebApi.Models
{
    public class MessageDetail
    {
        public MessageDetail()
        {
        }

        public MessageDetail(Message message)
        {
            this.Message = message;
        }

        public Message Message { get; set; }
        public bool IsPalindrome { get; set; }
    }
}