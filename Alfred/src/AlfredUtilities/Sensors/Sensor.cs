using System;
using System.Collections.Generic;

namespace AlfredUtilities.Sensors
{
    /// <summary>
    /// Defines what a sensor is.
    ///
    /// Stores all values of a sensor.
    /// </summary>
    public class Sensor : AlfredBase
    {
        #region Public Fields

        public static readonly Sensor Null = new NullSensor();

        #endregion Public Fields

        #region Public Constructors

        public Sensor() : this(string.Empty)
        {
        }

        public Sensor(string name)
        {
            Name = name;
        }

        public Sensor(Sensor sensorToClone)
        {
            Name = sensorToClone.Name;
            Id = sensorToClone.Id;
            Data = sensorToClone.Data; // Todo deep copy.
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// List of all values conained in the sensor.
        /// </summary>
        public List<SensorData> Data { get; set; } = new List<SensorData>();

        /// <summary>
        /// Id of the sensor.
        /// Needed to find it.
        ///
        /// The Guid type is choose to avoid painfully id attribution.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Pretty name of the sensor.
        ///
        /// Mainly used in GUI.
        /// </summary>
        public string Name { get; set; }

        #endregion Public Properties

        #region Protected Methods

        // todo : internal set to avoid plugins to change id.
        protected override void DisposeManagedObjects()
        {
            foreach (SensorData sensorData in Data)
            {
                sensorData.Dispose();
            }
        }

        protected override void DisposeUnmanagedObjects()
        {
            // Nothing to do.
        }

        #endregion Protected Methods

        #region Private Classes

        private sealed class NullSensor : Sensor
        {
            #region Public Constructors

            public NullSensor() : base("Null Sensor")
            {
                Id = Guid.Empty;
            }

            #endregion Public Constructors
        }

        #endregion Private Classes
    }
}
