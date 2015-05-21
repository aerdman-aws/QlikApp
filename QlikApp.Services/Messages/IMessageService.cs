using QlikApp.Data.Messages.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QlikApp.Services.Messages
{
    /// <summary>
    /// A service to access, create, or delete messages in the sytem
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// Creates a new message in the system with the specified message body
        /// </summary>
        /// <param name="messageBody">The message body, or actual text, of the message</param>
        /// <returns>The newly created message along with its unique identifier and body</returns>
        Message Create(string messageBody);

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
        /// Removes the message from the system that matches the given message id
        /// </summary>
        /// <param name="id">The unique id of the message to remove</param>
        /// <returns>The message that was removed from the system, or null if a message with id could not be found</returns>
        Message Remove(int id);

        /// <summary>
        /// Determines whether the given message's text is a palindrome.
        /// A message is a palindrome if the text is the same forwards or backwards.
        /// NOTE: White space is ignored, and the check is case insensitive. 
        /// </summary>
        /// <param name="message">The message to be checked</param>
        /// <returns>True if the message text is a palindrome, otherwise False</returns>
        bool IsPalindrome(Message message);
    }
}
