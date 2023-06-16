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
                _ = new PluginPathFinder(loggerFactory: null!, config: configurationMock.Object);
            });
        }

        [Fact]
        [Trait("Category", "PluginStore")]
        public void ConstructorNullConfigurationTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new PluginPathFinder(loggerFactory: new NullLoggerFactory(), config: null!);
            });
        }
    }
}
