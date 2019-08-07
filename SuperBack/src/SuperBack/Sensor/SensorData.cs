namespace SuperBack.Sensor
{
    /// <summary>
    /// Stores data value for sensors.
    /// 
    /// </summary>
    /// <typeparam name="T">Value has no pre-defined types</typeparam>    
    public class SensorData<T>
    {
        /// <summary>
        /// Name of the data.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value of the data.
        /// </summary>
        public T Value { get; set; }

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
        public SensorData(string name, T initialValue)
        {
            Name = name;
            Value = initialValue;
        }
    }
}
