using Alfred.Messages;
using Alfred.SensorsService;

using AlfredUtilities.Messages;
using AlfredUtilities.Sensors;

using FluentAssertions;

using Microsoft.Extensions.Logging.Abstractions;

using System;

using Xunit;

namespace Alfred.Tests
{
    public class SensorsServiceTests
    {
        #region Private Fields

        private readonly ISensorsService sensorService;

        #endregion Private Fields

        #region Public Constructors

        public SensorsServiceTests()
        {
            IMessageDispatcher dispatcher = new MessageDispatcher();
            sensorService = new SensorsService.SensorsService(dispatcher, new NullLoggerFactory());
        }

        #endregion Public Constructors

        #region Public Methods

        [Fact]
        [Trait("Category", "SensorService")]
        public void ConstructorNullDispatcherTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new SensorsService.SensorsService(dispatcher: null!, loggerFactory: new NullLoggerFactory());
            });
        }

        [Fact]
        [Trait("Category", "SensorService")]
        public void ConstructorNullLoggerTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new SensorsService.SensorsService(dispatcher: new MessageDispatcher(), loggerFactory: null!);
            });
        }

        [Fact]
        [Trait("Category", "SensorService")]
        public void AddNewSensorTest()
        {
            Sensor sensor = new("Sensor42");

            Guid id = sensorService.Add(sensor);

            sensor.Id.Should().NotBe(Guid.Empty, "the service should have provided an id");
            id.Should().NotBe(Guid.Empty, "the service should have provided an id");
            sensor.Id.Should().Be(id, "the id of the sensor and the one returned by the service when the addition of the id should be the same");
            sensorService.Sensors.Should().HaveCount(1);
            sensorService.Sensors[0].Name.Should().Be("Sensor42");
        }

        [Fact]
        [Trait("Category", "SensorService")]
        public void AddSensorNullTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = sensorService.Add(null!);
            });

            sensorService.Sensors.Should().BeEmpty("the sensor is null, so no sensor should be added to the service");
        }

        [Fact]
        [Trait("Category", "SensorService")]
        public void AddNullSensorTest()
        {
            Sensor sensor = Sensor.Null;
            Guid id = sensorService.Add(sensor);

            id.Should().Be(Guid.Empty);
            sensorService.Sensors.Should().BeEmpty("the sensor is null, so no sensor should be added to the service");
        }

        [Fact]
        [Trait("Category", "SensorService")]
        public void AddSeveralSensorsTest()
        {
            Sensor sensor = new("A");
            Guid guid = sensorService.Add(sensor);

            Sensor sensor2 = new("B");
            sensor2.Data.Add(new SensorData("B Data", 42));

            Guid guid2 = sensorService.Add(sensor2);

            guid.Should().NotBe(guid2, "the two sensors id should be different");
            sensorService.Sensors.Should().HaveCount(2);
        }

        [Fact]
        [Trait("Category", "SensorService")]
        public void DeleteSensorTest()
        {
            Sensor sensor = new("Sensor To Delete");
            Guid id = sensorService.Add(sensor);

            sensorService.Sensors.Should().HaveCount(1);

            bool result = sensorService.Delete(id);
            result.Should().BeTrue("deleting the sensor should works");
            sensorService.Sensors.Should().BeEmpty();
        }

        [Fact]
        [Trait("Category", "SensorService")]
        public void DeleteUnkownSensorTest()
        {
            Sensor sensor = new("UselessSensor");
            sensorService.Add(sensor);

            bool result = sensorService.Delete(Guid.NewGuid());
            result.Should().BeFalse("deleting an unknown sensor should do nothing");
            sensorService.Sensors.Should().HaveCount(1);
        }

        [Fact]
        [Trait("Category", "SensorService")]
        public void ReadTest()
        {
            Sensor sensor1 = new("UselessSensor1");
            sensorService.Add(sensor1);

            Sensor sensor2 = new("Sensor To get");
            Guid id = sensorService.Add(sensor2);

            Sensor sensor3 = new("UselessSensor2");
            sensorService.Add(sensor3);

            Sensor sensor = sensorService.Read(id);
            sensor.Should().NotBeNull();

            sensor.Should().Be(sensor2);
        }

        [Fact]
        [Trait("Category", "SensorService")]
        public void ReadWithUnknownIdTest()
        {
            Sensor sensor1 = new("UselessSensor");
            sensorService.Add(sensor1);

            Guid id = Guid.NewGuid();

            Sensor sensor = sensorService.Read(id);
            sensor.Should().NotBeNull("event if no sensor are found the service should return NullSensor");
            sensor.Should().Be(Sensor.Null, "if the id is not found in the service it should return NullSensor");
        }

        [Fact]
        [Trait("Category", "SensorService")]
        public void UpdateSensorTest()
        {
            Sensor sensor = new("Not updated sensor");
            Guid id = sensorService.Add(sensor);

            sensor.Name = "Updated sensor";

            bool result = sensorService.Update(id, sensor);

            Sensor sensorFromService = sensorService.Read(id);

            result.Should().BeTrue();
            sensorFromService.Name.Should().Be("Updated sensor");
        }

        [Fact]
        [Trait("Category", "SensorService")]
        public void UpdateNullTest()
        {
            Sensor sensor = new("Not updated sensor");
            Guid id = sensorService.Add(sensor);

            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = sensorService.Update(id, null!);
            });
        }

        [Fact]
        [Trait("Category", "SensorService")]
        public void UpdateNullSensorTest()
        {
            Sensor sensor = new("Not updated sensor");
            Guid id = sensorService.Add(sensor);

            bool result = sensorService.Update(id, Sensor.Null);

            Sensor sensorFromService = sensorService.Read(id);

            result.Should().BeTrue();
            sensorFromService.Name.Should().Be(Sensor.Null.Name);
        }

        [Fact]
        [Trait("Category", "SensorService")]
        public void UpdateWrongIdTest()
        {
            Sensor sensor = new("Not updated sensor");
            Guid id = sensorService.Add(sensor);

            Sensor newSensor = new(sensor);
            newSensor.Name = "Updated sensor";

            Guid newId = Guid.NewGuid();

            bool result = sensorService.Update(newId, newSensor);

            Sensor sensorFromService = sensorService.Read(id);

            result.Should().BeFalse();
            sensorFromService.Name.Should().Be("Not updated sensor");
        }

        [Fact]
        [Trait("Category", "SensorService")]
        public void AddAnExistingSensorTest()
        {
            Sensor sensor = new("Not updated sensor");
            Guid id = sensorService.Add(sensor);

            sensor.Name = "Updated sensor";

            Guid id2 = sensorService.Add(sensor);

            Sensor sensorFromService = sensorService.Read(id);

            id2.Should().Be(id);
            sensorFromService.Name.Should().Be("Updated sensor");
        }

        [Fact]
        [Trait("Category", "SensorService")]
        public void ConsumeNullMessageTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                (sensorService as SensorsService.SensorsService)?.Consume(null!);
            });
        }

        #endregion Public Methods
    }
}
