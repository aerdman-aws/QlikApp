using QlikApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QlikApp.Controllers
{
    public class MessagesController : ApiController
    {
        private Message[] messages = new Message[] 
        { 
            new Message { Id = 1, Body = "Tomato Soup" }, 
            new Message { Id = 2, Body = "Yo-yo" }, 
            new Message { Id = 3, Body = "Hammer" } 
        };

        public IEnumerable<Message> GetAll()
        {
            return messages;
        }

        public IHttpActionResult Get(int id)
        {
            var message = messages.FirstOrDefault((p) => p.Id == id);
            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);
        }
    }
}
