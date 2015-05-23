using QlikApp.Web.WebApi.Converters;
using QlikApp.Web.WebApi.Models;
using QlikApp.Services;
using QlikApp.Services.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QlikApp.Web.WebApi.Controllers
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

        /// <summary>
        /// Retrieves all the messages in the system
        /// </summary>
        /// <returns>All the messages in the system</returns>
        public IEnumerable<Message> GetAll()
        {
            return MessageService.GetAll().Select(m => MessageConverter.Convert(m));
        }

        /// <summary>
        /// Retrieves the message from the system that matches the given message id
        /// </summary>
        /// <param name="id">The unique id of the message to retrieve</param>
        /// <returns>The message that matches the given id, or NotFound if a message with id could not be found</returns>
        public IHttpActionResult Get(int id)
        {
            var message = MessageService.Get(id);
            if (message == null)
            {
                return NotFound();
            }

            var messageDetail = new MessageDetail
            {
                Message = MessageConverter.Convert(message),
                IsPalindrome = MessageService.IsPalindrome(message)
            };

            return Ok(messageDetail);
        }

        /// <summary>
        /// Creates a new message in the system with the specified message data
        /// </summary>
        /// <param name="data">The message data, or actual text, of the message</param>
        /// <returns>The newly created message along with its unique identifier and body</returns>
        public HttpResponseMessage Post([FromBody] Message data)
        {
            var message = MessageService.Create(data.Body);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, MessageConverter.Convert(message));
            return response;
        }

        /// <summary>
        /// Deletes the message from the system that matches the given message id
        /// </summary>
        /// <param name="id">The unique id of the message to remove</param>
        /// <returns>The message that was deleted from the system, or NotFound if a message with id could not be found</returns>
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
