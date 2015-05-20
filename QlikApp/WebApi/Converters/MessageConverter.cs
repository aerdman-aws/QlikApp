using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QlikApp.Web.WebApi.Converters
{
    public class MessageConverter
    {
        public Models.Message Convert(Data.Messages.Model.Message message)
        {
            return new Models.Message
            {
                Id = message.Id,
                Body = message.Body
            };
        }

        public Data.Messages.Model.Message Convert(Models.Message message)
        {
            return new Data.Messages.Model.Message
            {
                Id = message.Id,
                Body = message.Body
            };
        }
    }
}