using Alfred.Messages;

using AlfredUtilities.Messages;

using Moq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace Alfred.Tests
{
    public class MessageDispatcherTest
    {
        private MessageDispatcher _dispatcher;

        public MessageDispatcherTest()
        {
            _dispatcher = new();
        }

        [Fact]
        [Trait("Category", "MessageDispatcher")]
        public void EnqueueMessageNullMessageTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _dispatcher.EnqueueMessage(null!);
            });
        }

        [Fact]
        [Trait("Category", "MessageDispatcher")]
        public void RegisterNullTopicTest()
        {
            Mock<IMessageListener> listenerMock = new Mock<IMessageListener>();

            Assert.Throws<ArgumentNullException>(() =>
            {
                _dispatcher.Register(null!, listenerMock.Object);
            });
        }

        [Fact]
        [Trait("Category", "MessageDispatcher")]
        public void RegisterNullListenerTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _dispatcher.Register("topicName", null!);
            });
        }
    }
}
