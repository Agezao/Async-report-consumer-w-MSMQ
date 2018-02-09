using MsmqConsumer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MsmqConsumer.Messaging
{
    /// <summary>
    /// Class for managing the Queue status and Messaging
    /// </summary>
    public class MQueue<T>
    {
        /// <summary>
        /// Prop that holds the MSMQ instance
        /// </summary>
        public MessageQueue Queue { get; set; }

        public MQueue()
        {
            this.AssertQueueExistence();
        }

        ~MQueue() {
            this.Dispose();
        }

        /// <summary>
        /// Assert that a Queue exists.
        /// </summary>
        /// <param name="queueName">Optional queue name to check. If not provided falls back to the config defaults</param>
        /// <param name="queueDescription">Optional queue description when crating a new one. If not provided falls back to the config defaults</param>
        /// <param name="isPublic">Whether or not a queue should be considered private or public</param>
        public void AssertQueueExistence(string queueName = null, string queueDescription = null, bool isPublic = false) {
            var queuePath =  $@".\{(isPublic ? "Public" : "Private")}$\{ queueName ?? ConfigHelper.QueueName }";

            if (!MessageQueue.Exists(queuePath))
                MessageQueue.Create(queuePath);

            Queue = new MessageQueue(queuePath);
            Queue.Label = queueDescription ?? ConfigHelper.QueueDescription;
            Queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
        }

        /// <summary>
        /// Dispose the Queue object after usage
        /// </summary>
        public void Dispose() {
            if(Queue != null)
                Queue.Dispose();
        }
    }
}
