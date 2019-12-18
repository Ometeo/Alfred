using System;

namespace AlfredUtilities.Sensors
{
    /// <summary>
    /// Stores data value for sensors.
    /// 
    /// <para>Value is stored in object type for handling every possible types of value.</para>
    /// 
    /// <para>The type of the value is also stored.</para>
    /// 
    /// </summary>   
    public class SensorData : AlfredBase
    {
        /// <summary>
        /// Name of the data.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Value of the data.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Type of the value.
        /// </summary>
        public Type Type
        {
            get { return Value.GetType(); }           
        }

        /// <summary>
        /// Default constructor.
        /// 
        /// <para>Name is Sensor by default.</para>
        /// 
        /// <para>Value takes the default value of its type.</para>
        /// </summary>
        public SensorData() : this("Sensor", default)
        {

        }

        /// <summary>
        /// Parametrized constructor of SensorData.
        /// </summary>
        /// <param name="name">The name of the data.</param>
        /// <param name="initialValue">The initial value of the data.</param>
        public SensorData(string name, object initialValue)
        {
            Name = name;
            Value = initialValue;
        }

        protected override void DisposeManagedObjects()
        {
            Console.WriteLine("            * Dispose Managed objects for sensorData");
            if(Value is IDisposable)
            {
                ((IDisposable)Value).Dispose();
            }            
        }

        protected override void DisposeUnmanagedObjects()
        {
            Console.WriteLine("            * Dispose Unmanaged objects for sensorData");
        }
    }
}
