using Alfred.Plugins;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;

using Moq;

using System;

using Xunit;

namespace Alfred.Tests
{
    public class PluginPathFinderTests
    {
        [Fact]
        [Trait("Category", "PluginStore")]
        public void ConstructorNullLoggerTest()
        {
            Mock<IConfiguration> configurationMock = new();

            Assert.Throws<ArgumentNullException>(() =>
            {
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
                _ = new PluginPathFinder(loggerFactory: null, config: configurationMock.Object);
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            });
        }

        [Fact]
        [Trait("Category", "PluginStore")]
        public void ConstructorNullConfigurationTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
                _ = new PluginPathFinder(loggerFactory: new NullLoggerFactory(), config: null);
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            });
        }
    }
}
