﻿using Alfred;
using Alfred.Plugins;
using Alfred.SensorsService;

using AlfredUtilities.Messages;
using AlfredUtilities.Sensors;

using Microsoft.Extensions.Logging;

using Moq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace AlfredUnitTest
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
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
                _ = new MainApp(
                    null,
                    _messageDispatcherMock.Object,
                    _pluginStorerMock.Object,
                    _loggerFactoryMock.Object);
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            });
        }

        [Fact]
        [Trait("Category", "MainApp")]
        public void MainAppConstructorNullMessageDispatcherTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
                _ = new MainApp(
                    _sensorsServiceMock.Object,
                    null,
                    _pluginStorerMock.Object,
                    _loggerFactoryMock.Object);
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            });
        }

        [Fact]
        [Trait("Category", "MainApp")]
        public void MainAppConstructorNullPluginStoreTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
                _ = new MainApp(
                    _sensorsServiceMock.Object,
                    _messageDispatcherMock.Object,
                    null,
                    _loggerFactoryMock.Object);
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            });
        }

        [Fact]
        [Trait("Category", "MainApp")]
        public void MainAppConstructorNullLoggerFactoryTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
                _ = new MainApp(
                    _sensorsServiceMock.Object,
                    _messageDispatcherMock.Object,
                    _pluginStorerMock.Object,
                    null);
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            });
        }
    }
}
