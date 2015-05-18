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
        private static List<Message> messages = new List<Message>(new[]
        { 
            new Message { Id = 1, Body = "Tomato Soup" }, 
            new Message { Id = 2, Body = "Yo-yo" }, 
            new Message { Id = 3, Body = "Hammer" } 
        });

        public IEnumerable<Message> GetAll()
        {
            return messages;
        }

        public IHttpActionResult Get(int id)
        {
            var message = messages.FirstOrDefault(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }

            var messageDetail = new MessageDetail(message);
            return Ok(messageDetail);
        }

        public HttpResponseMessage Post([FromBody] Message data)
        {
            var message = new Message
            {
                Id = messages.Max(m => m.Id) + 1,
                Body = data.Body
            };
            messages.Add(message);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, messages);
            return response;
        }

        public IHttpActionResult Delete(int id)
        {
            var message = messages.FirstOrDefault(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }

            messages.Remove(message);

            return Ok(message);
        }
    }
}
