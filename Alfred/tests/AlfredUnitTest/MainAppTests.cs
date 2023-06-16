using Alfred.Plugins;
using Alfred.SensorsService;

using AlfredUtilities.Messages;

using Microsoft.Extensions.Logging;

using Moq;

using System;

using Xunit;

namespace Alfred.Tests
{
    public class MainAppTests
    {
        private readonly Mock<ISensorsService> _sensorsServiceMock;
        private readonly Mock<IMessageDispatcher> _messageDispatcherMock;
        private readonly Mock<IPluginStore> _pluginStorerMock;
        private readonly Mock<ILoggerFactory> _loggerFactoryMock;

        public MainAppTests()
        {
            _sensorsServiceMock = new Mock<ISensorsService>();
            _messageDispatcherMock = new Mock<IMessageDispatcher>();
            _pluginStorerMock = new Mock<IPluginStore>();
            _loggerFactoryMock = new Mock<ILoggerFactory>();
        }

        [Fact]
        [Trait("Category", "MainApp")]
        public void MainAppConstructorNullSensorServiceTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new MainApp(
                    null!,
                    _messageDispatcherMock.Object,
                    _pluginStorerMock.Object,
                    _loggerFactoryMock.Object);
            });
        }

        [Fact]
        [Trait("Category", "MainApp")]
        public void MainAppConstructorNullMessageDispatcherTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new MainApp(
                    _sensorsServiceMock.Object,
                    null!,
                    _pluginStorerMock.Object,
                    _loggerFactoryMock.Object);
            });
        }

        [Fact]
        [Trait("Category", "MainApp")]
        public void MainAppConstructorNullPluginStoreTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new MainApp(
                    _sensorsServiceMock.Object,
                    _messageDispatcherMock.Object,
                    null!,
                    _loggerFactoryMock.Object);
            });
        }

        [Fact]
        [Trait("Category", "MainApp")]
        public void MainAppConstructorNullLoggerFactoryTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new MainApp(
                    _sensorsServiceMock.Object,
                    _messageDispatcherMock.Object,
                    _pluginStorerMock.Object,
                    null!);
            });
        }
    }
}
