using AlfredUtilities;
using System;
using System.Collections.Generic;

namespace SuperBack.Sensor
{
    /// <summary>
    /// Defines what a sensor is.
    /// 
    /// Stores all values of a sensor.
    /// </summary>
    public class Sensor : AlfredBase
    {
        public static readonly Sensor Null = new NullSensor();

        private class NullSensor : Sensor
        {
            public NullSensor() : base("Null Sensor")
            {
                
            }
        }

        /// <summary>
        /// Id of the sensor. 
        /// Needed to find it.
        /// 
        /// The Guid type is choose to avoid painfully id attribution.
        /// </summary>
        public Guid Id { get; internal set; }        

        /// <summary>
        /// Pretty name of the sensor.
        /// 
        /// Mainly used in GUI.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of all values conained in the sensor.
        /// </summary>
        public List<SensorData> Data { get; set; } = new List<SensorData>();

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

        protected override void DisposeManagedObjects()
        {
            Console.WriteLine("        * Dispose Managed objects for sensor");
            foreach (SensorData sensorData in Data)
            {
                sensorData.Dispose();
            }
        }

        protected override void DisposeUnmanagedObjects()
        {
            Console.WriteLine("        * Dispose Unmanaged objects for sensor");
        }
    }
}
