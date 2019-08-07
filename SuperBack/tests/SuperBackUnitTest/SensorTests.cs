using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperBack.Sensor;

namespace SuperBackUnitTest
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
            SensorData<int> intData = new SensorData<int>("Int data", 42);
            Assert.AreEqual(42, intData.Value, "Value of intData should be 42");
            Assert.AreEqual("Int data", intData.Name, "Name of intData should be \"Int data\"");

            // Double value.
            SensorData<double> doubleData = new SensorData<double>("Double data", 152.0);
            Assert.AreEqual(152.0, doubleData.Value, "Value of doubleData should be 152.0");

            // String value.
            SensorData<string> stringData = new SensorData<string>("String data", "Hello from unit test");
            Assert.AreEqual("Hello from unit test", stringData.Value, "Value of stringData should be \"Hello from unit test\"");

            // Boolean value.
            SensorData<bool> boolData = new SensorData<bool>("Bool data", true);
            Assert.AreEqual(true, boolData.Value, "Value of boolData should be true");

            boolData.Value = false;
            Assert.AreEqual(false, boolData.Value, "Value of boolData should be false");
        }       
    }
}
