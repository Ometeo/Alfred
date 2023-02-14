using FluentAssertions;
using System;
using Xunit;

using AlfredUtilities.Sensors;

namespace AlfredUnitTest
{
    public class SensorTests
    {
        /// <summary>
        /// Test the instanciation's correctness for SensorData with several types. The interresting point here is on the type checking.
        /// </summary>
        [Theory]
        [Trait("Category", "Sensor")]
        [Trait("Category", "SensorData")]
        [InlineData("Int data", 42, typeof(int))]
        [InlineData("Double data", 152.3, typeof(double))]
        [InlineData("String data", "Hello from unit test", typeof(string))]
        [InlineData("Bool data", true, typeof(bool))]
        [InlineData("Bool data", false, typeof(bool))]
        public void SensorDataCreationTest(string name, object value, Type expectedType)
        {
            SensorData data = new(name, value);
            data.Value.Should().Be(value, $"value of the sensorData should be {value}");
            data.Type.Should().Be(expectedType, $"as sensorData's value is {value}, its type should be {expectedType}");
            data.Name.Should().Be(name);
        }

        /// <summary>
        /// Test the instanciation's correctness for Sensor.
        /// </summary>
        [Fact]
        [Trait("Category", "Sensor")]
        [Trait("Category", "SensorData")]
        public void SensorCreationTest()
        {
            Sensor sensor = new ("Sensor #1");
            SensorData sensorData = new ("Count", -15);
            sensor.Data.Add(sensorData);
            sensor.Name.Should().Be("Sensor #1");
            sensor.Data.Should().HaveCount(1, "sensor have 1 sensor data");
            sensor.Data[0].Value.Should().Be(-15);
        }

        /// <summary>
        /// Test the copy constructor.
        /// </summary>
        [Fact]
        [Trait("Category", "Sensor")]
        [Trait("Category", "SensorData")]
        public void SensorCopyTest()
        {
            Sensor sensor = new ("Sensor #1");
            SensorData sensorData = new("Count", -15);
            sensor.Data.Add(sensorData);

            Sensor copy = new (sensor);
            copy.Name.Should().Be("Sensor #1", "copy's name should be the same than the copied sensor");
            copy.Data.Should().HaveCount(1, "the copy should have the same number of data");
            copy.Data[0].Value.Should().Be(-15, "the data value of the copy should be the same than the copied one");
        }
    }
}