﻿using AlfredUtilities.Messages;
using System.Collections.Generic;

namespace Alfred.Messages
{
    public class MessageDispatcher : IMessageDispatcher
    {
        private readonly Queue<Message> messages = new Queue<Message>();
        private readonly object messageLock = new object();

        private readonly Dictionary<string, HashSet<IMessageListener>> registeredListener = new Dictionary<string, HashSet<IMessageListener>>();

        public Message DequeueMessage()
        {
            Message message;
            lock (messageLock)
            {
                message = messages.TryDequeue(out message) ? message : Message.Null;

                if (!Message.Null.Equals(message) && registeredListener.TryGetValue(message.Topic, out HashSet<IMessageListener> listeners))
                {
                    foreach (IMessageListener listener in listeners)
                    {
                        listener.Consume(message);
                    }
                }
            }

            return message;
        }

        public void EnqueueMessage(Message message)
        {
            lock (messageLock)
            {
                messages.Enqueue(message);
            }
        }

        /// <summary>
        /// Register a IMessageListener to a specific topic of the message dispatcher.
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="listener"></param>
        /// <returns>
        /// true if the element is correctly registered; false if the element is already present.
        /// </returns>
        public bool Register(string topic, IMessageListener listener)
        {
            if (!registeredListener.TryGetValue(topic, out HashSet<IMessageListener> listeners))
            {
                listeners = new HashSet<IMessageListener>();
                registeredListener.Add(topic, listeners);
            }

            return listeners.Add(listener);
        }
    }
}