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
        #region Public Constructors

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

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Name of the data.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Type of the value.
        /// </summary>
        public Type Type
        {
            get { return Value.GetType(); }
        }

        /// <summary>
        /// Value of the data.
        /// </summary>
        public object Value { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override void DisposeManagedObjects()
        {
            if (Value is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        protected override void DisposeUnmanagedObjects()
        {
            // Nothing to do.
        }

        #endregion Protected Methods
    }
}
