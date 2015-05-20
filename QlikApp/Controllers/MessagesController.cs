using QlikApp.Converters;
using QlikApp.Models;
using QlikApp.Services;
using QlikApp.Services.Messages;
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
        public MessagesController()
        {
            var serviceFactory = new ServiceFactory();
            MessageService = serviceFactory.GetMessageService();
            MessageConverter = new MessageConverter();
        }

        internal IMessageService MessageService { get; set; }
        internal MessageConverter MessageConverter { get; set; }

        public IEnumerable<Message> GetAll()
        {
            return MessageService.GetAll().Select(m => MessageConverter.Convert(m));
        }

        public IHttpActionResult Get(int id)
        {
            var message = MessageService.Get(id);
            if (message == null)
            {
                return NotFound();
            }

            var messageDetail = new MessageDetail(MessageConverter.Convert(message)); //TODO
            return Ok(messageDetail);
        }

        public HttpResponseMessage Post([FromBody] Message data)
        {
            var message = MessageService.Create(data.Body);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, MessageConverter.Convert(message));
            return response;
        }

        public IHttpActionResult Delete(int id)
        {
            var removedMessage = MessageService.Remove(id);
            if (removedMessage == null)
            {
                return NotFound();
            }

            return Ok(MessageConverter.Convert(removedMessage));
        }
    }
}
