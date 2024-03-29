﻿using Alfred.Controllers;
using Alfred.SensorsService;

using AlfredUtilities.Messages;
using AlfredUtilities.Sensors;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;

using Moq;

using System;
using System.Collections.Generic;

using Xunit;

namespace Alfred.Tests
{
    public class SensorControllerTests
    {
        #region Private Fields

        private readonly Mock<IMessageDispatcher> _messageDispatcherMock;
        private readonly List<Sensor> _sensors;
        private ISensorsService _sensorsService;

        #endregion Private Fields

        #region Public Constructors

        public SensorControllerTests()
        {
            _sensors = new();
            Sensor sensor = new()
            {
                Name = "Sensor1",
                Id = Guid.NewGuid(),
                Data = new()
            };

            sensor.Data.Add(new("SensorValue", 42));
            _sensors.Add(sensor);

            Sensor sensor2 = new()
            {
                Name = "Sensor2",
                Id = Guid.NewGuid(),
                Data = new()
            };
            sensor2.Data.Add(new("OtherValue", "Data Value"));
            _sensors.Add(sensor2);

            _messageDispatcherMock = new();
            _messageDispatcherMock.SetupAllProperties();

            _sensorsService = new SensorsService.SensorsService(_messageDispatcherMock.Object, NullLoggerFactory.Instance);
            _sensorsService.Add(sensor);
            _sensorsService.Add(sensor2);
        }

        #endregion Public Constructors

        #region Public Methods

        [Fact]
        [Trait("Category", "SensorController")]
        [Trait("Category", "SensorController_Get")]
        public void GetAllSensorsTest()
        {
            SensorController controller = new(_sensorsService);
            var result = controller.GetAll();

            result.Should().BeOfType(typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            var list = (okResult?.Value as List<Sensor>) ?? new List<Sensor>();
            list.Should().HaveCount(2);
            list.Should().Contain(x => x == _sensors[0]);
            list.Should().Contain(x => x == _sensors[1]);
        }

        [Fact]
        [Trait("Category", "SensorController")]
        [Trait("Category", "SensorController_Get")]
        public void GetAllSensorsWithEmptyTest()
        {
            _sensorsService = new SensorsService.SensorsService(_messageDispatcherMock.Object, NullLoggerFactory.Instance);

            SensorController controller = new(_sensorsService);
            var result = controller.GetAll();

            result.Should().BeOfType(typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            var list = (okResult?.Value as List<Sensor>) ?? new List<Sensor>();
            list.Should().HaveCount(0);
        }

        [Fact]
        [Trait("Category", "SensorController")]
        [Trait("Category", "SensorController_Get")]
        public void GetSensorByIdEmptyListTest()
        {
            _sensorsService = new SensorsService.SensorsService(_messageDispatcherMock.Object, NullLoggerFactory.Instance);

            SensorController controller = new(_sensorsService);
            var result = controller.Get(Guid.NewGuid());

            result.Should().BeOfType(typeof(NotFoundResult));
        }

        [Fact]
        [Trait("Category", "SensorController")]
        [Trait("Category", "SensorController_Get")]
        public void GetSensorByIdTest()
        {
            SensorController controller = new(_sensorsService);
            var result = controller.Get(_sensors[0].Id);

            result.Should().BeOfType(typeof(OkObjectResult));
            var okResult = result as OkObjectResult;

            var resultSensor = (okResult?.Value as Sensor) ?? Sensor.Null;
            resultSensor.Should().Be(_sensors[0]);
        }

        [Fact]
        [Trait("Category", "SensorController")]
        [Trait("Category", "SensorController_Get")]
        public void GetSensorByIdUnknownGuidTest()
        {
            SensorController controller = new(_sensorsService);
            var result = controller.Get(Guid.NewGuid());

            result.Should().BeOfType(typeof(NotFoundResult));
        }

        [Fact]
        [Trait("Category", "SensorController")]
        [Trait("Category", "SensorController_Put")]
        public void UpdateSensorByIdTest()
        {
            SensorController controller = new(_sensorsService);
            Sensor updateSensor = new(_sensors[0])
            {
                Name = "NewSensorName"
            };

            updateSensor.Data[0].Value = 42;

            var result = controller.Update(_sensors[0].Id, updateSensor);
            result.Should().BeOfType(typeof(OkObjectResult));
            var okResult = result as OkObjectResult;

            var resultSensor = (okResult?.Value as Sensor) ?? Sensor.Null;
            resultSensor.Should().Be(_sensors[0]);
            resultSensor.Name.Should().Be(_sensors[0].Name);
            resultSensor.Data.Should().HaveCount(_sensors[0].Data.Count);
            resultSensor.Data[0].Name.Should().Be(_sensors[0].Data[0].Name);
            resultSensor.Data[0].Value.Should().Be(42);
        }

        [Fact]
        [Trait("Category", "SensorController")]
        [Trait("Category", "SensorController_Put")]
        public void UpdateSensorByIdWithUnknownIdTest()
        {
            SensorController controller = new(_sensorsService);
            Sensor updateSensor = new(_sensors[0])
            {
                Name = "NewSensorName",
                Id = Guid.NewGuid()
            };

            var result = controller.Update(updateSensor.Id, updateSensor);
            result.Should().BeOfType(typeof(NotFoundResult));
        }

        [Fact]
        [Trait("Category", "SensorController")]
        [Trait("Category", "SensorController_Put")]
        public void UpdateSensorByIdWithWrongIdTest()
        {
            SensorController controller = new(_sensorsService);
            Sensor updateSensor = new(_sensors[0])
            {
                Name = "NewSensorName"
            };

            var result = controller.Update(Guid.NewGuid(), updateSensor);
            result.Should().BeOfType(typeof(BadRequestResult));
        }

        #endregion Public Methods
    }
}
