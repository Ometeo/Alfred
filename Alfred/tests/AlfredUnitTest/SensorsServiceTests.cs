using Alfred.Messages;
using Alfred.Sensors;
using AlfredUtilities.Messages;
using AlfredUtilities.Sensors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlfredUnitTest
{
    [TestClass]
    public class SensorsServiceTests
    {
        private ISensorService sensorService;

        [TestInitialize]
        public void Init()
        {
            IMessageDispatcher dispatcher = new MessageDispatcher();
            sensorService = new SimpleSensorService(dispatcher);
        }

        [TestMethod, TestCategory("SensorService")]
        public void AddNullSensorTest()
        {
            Sensor sensor = null;
            Guid id = sensorService.Add(sensor);

            Assert.AreEqual(Guid.Empty, id, "Returned id should be empty.");
            Assert.AreEqual(0, sensorService.Sensors.Count, "Service shouldn't have sensor.");
        }

        [TestMethod, TestCategory("SensorService")]
        public void AddNewSensorTest()
        {
            Sensor sensor = new Sensor("BatSensor");

            Guid id = sensorService.Add(sensor);

            Assert.AreNotEqual(Guid.Empty, sensor.Id, "Sensor id should not be empty");
            Assert.AreNotEqual(Guid.Empty, id, "Returned id should not be empty");
            Assert.AreEqual(sensor.Id, id, "Ids should be equals");
            Assert.AreEqual(1, sensorService.Sensors.Count, "Service should have 1 sensor");
            Assert.AreEqual("BatSensor", sensorService.Sensors[0].Name, "Sensor's name should be correct");
        }

        [TestMethod, TestCategory("SensorService")]
        public void AddAlreadyAddedSensorTest()
        {
            Sensor sensor = new Sensor("BatSensor2");
            Guid guid = sensorService.Add(sensor);

            Sensor sensor2 = new Sensor(sensor)
            {
                Name = "New sensor's name"
            };

            Guid guid2 = sensorService.Add(sensor2);

            Assert.AreEqual(guid, guid2, "Sensor id should not change");
            Assert.AreEqual(1, sensorService.Sensors.Count, "Service should have 1 sensor");
            Assert.AreEqual("New sensor's name", sensorService.Sensors[0].Name, "Sensor's name should have changed");
        }

        [TestMethod, TestCategory("SensorService")]
        public void AddSeveralSensorsTest()
        {
            Sensor sensor = new Sensor("A");
            Guid guid = sensorService.Add(sensor);

            Sensor sensor2 = new Sensor("B");
            sensor2.Data.Add(new SensorData("B Data", 42));

            Guid guid2 = sensorService.Add(sensor2);

            Assert.AreNotEqual(guid, guid2, "Sensors id shouldn't be equals");
            Assert.AreEqual(2, sensorService.Sensors.Count, "Service should have 1 sensor");
        }

        [TestMethod]
        public void DeleteUnkownSensorTest()
        {
            bool result = sensorService.Delete(Guid.NewGuid());
            Assert.IsFalse(result, "Delete should return false, it did nothing.");
        }

        [TestMethod]
        public void DeleteSensorTest()
        {
            Sensor sensor = new Sensor("Sensor To Delete");
            Guid id = sensorService.Add(sensor);

            Assert.AreEqual(1, sensorService.Sensors.Count, "Service should have 1 sensor");

            bool result = sensorService.Delete(id);
            Assert.IsTrue(result, "Delete should return false, it did nothing.");
            Assert.AreEqual(0, sensorService.Sensors.Count, "Service should have 0 sensor");
        }

        [TestMethod]
        public void ReadWithUnknownIdTest()
        {
            Guid id = Guid.NewGuid();

            Sensor sensor = sensorService.Read(id);
            Assert.AreEqual(Sensor.Null, sensor, "No null sensor should be returned");
        }

        [TestMethod]
        public void ReadTest()
        {
            Sensor sensor = new Sensor("Sensor To get");
            Guid id = sensorService.Add(sensor);

            Sensor sensor2 = sensorService.Read(id);
            Assert.IsNotNull(sensor2, "No sensor should be returned");
            Assert.AreSame(sensor, sensor2, "Sensors should be same");
        }

        [TestMethod]
        public void UpdateSensorTest()
        {
            Sensor sensor = new Sensor("Not updated sensor");
            Guid id = sensorService.Add(sensor);

            sensor.Name = "Updated sensor";

            bool result = sensorService.Update(id, sensor);

            Sensor sensorFromService = sensorService.Read(id);

            Assert.IsTrue(result, "Update should work");
            Assert.AreEqual("Updated sensor", sensorFromService.Name, "Sensor name should have changed.");
        }
    }
}