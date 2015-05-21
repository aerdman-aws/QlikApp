using QlikApp.Data.Messages.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QlikApp.Data.Messages
{
    /// <summary>
    /// An interface to access messages in the system
    /// </summary>
    public interface IMessageProvider
    {
        /// <summary>
        /// Creates a new message in the system.
        /// A unique id will be assigned to the message upon creation
        /// </summary>
        /// <param name="message">The message to create in the system</param>
        /// <returns>The newly created message along with its unique identifier and body</returns>
        Message Create(Message message);

        /// <summary>
        /// Retrieves the message from the system that matches the given message id
        /// </summary>
        /// <param name="id">The unique id of the message to retrieve</param>
        /// <returns>The message that matches the given id, or null if a message with id could not be found</returns>
        Message Get(int id);

        /// <summary>
        /// Retrieves all the messages in the system
        /// </summary>
        /// <returns>All the messages in the system</returns>
        IEnumerable<Message> GetAll();

        /// <summary>
        /// Removes the messages in the system
        /// </summary>
        /// <param name="message">The message to remove</param>
        /// <returns>True if the message was removed from the system, or False if the message did not exist in the system</returns>
        bool Remove(Message message);
    }
}
