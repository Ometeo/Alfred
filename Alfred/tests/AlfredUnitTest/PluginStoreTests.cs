using Moq;

using System;

using Xunit;
using AlfredUtilities.Messages;
using Microsoft.Extensions.Logging.Abstractions;
using Alfred.Plugins;

namespace Alfred.Tests
{
    public class PluginStoreTests
    {
        private readonly Mock<IPluginPathFinder> pluginPathFinderMock = new();
        private readonly Mock<IMessageDispatcher> messageDispatcherMock = new();

        [Fact]
        [Trait("Category", "PluginStore")]
        public void ConstructorNullPluginPathFinderTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new PluginStore(pluginPathFinder: null!, messageDispatcher: messageDispatcherMock.Object, loggerFactory: new NullLoggerFactory());
            });
        }

        [Fact]
        [Trait("Category", "PluginStore")]
        public void ConstructorNullMessageDispatcherTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new PluginStore(pluginPathFinder: pluginPathFinderMock.Object, messageDispatcher: null!, loggerFactory: new NullLoggerFactory());
            });
        }

        [Fact]
        [Trait("Category", "PluginStore")]
        public void ConstructorNullLoggerTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new PluginStore(pluginPathFinder: pluginPathFinderMock.Object, messageDispatcher: messageDispatcherMock.Object, loggerFactory: null!);
            });
        }
    }
}
