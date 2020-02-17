using AlfredUtilities.Sensors;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlfredUnitTest
{
    [TestClass]
    public class SensorTests
    {
        /// <summary>
        /// Test the correctness of instantiation for SensorData with several types.
        /// </summary>
        [TestMethod, TestCategory("Sensor")]
        public void SensorDataCreationTest()
        {
            // Integer value.
            SensorData intData = new SensorData("Int data", 42);
            Assert.AreEqual(42, intData.Value, "Value of intData should be 42");
            Assert.AreEqual(typeof(int), intData.Type, "Type of intData should be int32");
            Assert.AreEqual("Int data", intData.Name, "Name of intData should be \"Int data\"");

            // Double value.
            SensorData doubleData = new SensorData("Double data", 152.0);
            Assert.AreEqual(152.0, doubleData.Value, "Value of doubleData should be 152.0");
            Assert.AreEqual(typeof(double), doubleData.Type, "Type of doubleData should be double");

            // String value.
            SensorData stringData = new SensorData("String data", "Hello from unit test");
            Assert.AreEqual("Hello from unit test", stringData.Value, "Value of stringData should be \"Hello from unit test\"");
            Assert.AreEqual(typeof(string), stringData.Type, "Type of stringData should be string");

            // Boolean value.
            SensorData boolData = new SensorData("Bool data", true);
            Assert.AreEqual(true, boolData.Value, "Value of boolData should be true");
            Assert.AreEqual(typeof(bool), boolData.Type, "Type of boolData should be bool");

            boolData.Value = false;
            Assert.AreEqual(false, boolData.Value, "Value of boolData should be false");
        }

        [TestMethod, TestCategory("Sensor")]
        public void SensorCreationTest()
        {
            Sensor sensor = new Sensor("Sensor #1");
            SensorData sensorData = new SensorData("Count", -15);
            sensor.Data.Add(sensorData);
            Assert.AreEqual("Sensor #1", sensor.Name, "Name of sensor should be \"Sensor #1\"");
            Assert.AreEqual(1, sensor.Data.Count, "Sensor should have 1 data value");
            Assert.AreEqual(-15, sensor.Data[0].Value, "Value of data should be -15");
        }

        [TestMethod, TestCategory("Sensor")]
        public void SensorCopyTest()
        {
            Sensor sensor = new Sensor("Sensor #1");
            SensorData sensorData = new SensorData("Count", -15);
            sensor.Data.Add(sensorData);

            Sensor copy = new Sensor(sensor);
            Assert.AreEqual("Sensor #1", copy.Name, "Name of sensor should be \"Sensor #1\"");
            Assert.AreEqual(1, copy.Data.Count, "Sensor should have 1 data value");
            Assert.AreEqual(-15, copy.Data[0].Value, "Value of data should be -15");
        }
    }
}