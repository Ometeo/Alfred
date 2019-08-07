using System;
using System.Collections.Generic;

namespace SuperBack.Sensor
{
    /// <summary>
    /// Defines what a sensor is.
    /// 
    /// Stores all values of a sensor.
    /// </summary>
    public class Sensor
    {
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

        /// <summary>
        /// Dictionnary of all values conained in the sensor.
        /// </summary>
        public Dictionary<string, SensorData<object>> Data { get; set; } = new Dictionary<string, SensorData<object>>();
    }
}
