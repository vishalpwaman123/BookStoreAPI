using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Experimental.System.Messaging;

namespace BookStoreAPI.MSMQSender
{
    public class Sender
    {

        /// <summary>
        /// Define send method
        /// </summary>
        /// <param name="input">Passing input string</param>
        public void Send(String Message)
        {
            try
            {
                // Created the referrence of MessageQueue
                MessageQueue messageQueue = null;

                
                // Check if Message Queue Exists
                if (MessageQueue.Exists(@".\Private$\BookApiQueue"))
                {
                    messageQueue = new MessageQueue(@".\Private$\BookApiQueue");
                    messageQueue.Label = "Testing Queue";
                }
                else
                {
                    MessageQueue.Create(@".\Private$\BookApiQueue");
                    messageQueue = new MessageQueue(@".\Private$\BookApiQueue");
                    messageQueue.Label = "Newly Created Queue";
                }
                // Message send to Queue
                messageQueue.Send(Message);
               
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
