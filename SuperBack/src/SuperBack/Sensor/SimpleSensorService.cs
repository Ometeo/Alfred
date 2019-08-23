using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperBack.Sensor
{
    /// <summary>
    /// Simple implementation of the Sensor Service Interface.
    /// 
    /// <para>Use GUID for attributing Id's of sensors.</para>
    /// 
    /// 
    /// </summary>
    public class SimpleSensorService : ISensorService
    {
        private List<Sensor> sensors = new List<Sensor>();

        public IList<Sensor> Sensors => sensors; // Todo make it read-only, sensors should be updated only by interface methods.

        /// <summary>
        /// Add a sensor to the sensors list.
        /// 
        /// <para>If the list already contains a sensor with the same id it is replaced by the new one (update).</para>
        /// </summary>
        /// <param name="newSensor">Sensor to add.</param>
        /// <returns>The new of the added sensor. <code>Guid.Empty</code> if <code>newSensor</code> is <code>null</code></returns>
        public Guid Add(Sensor newSensor)
        {
            if (null != newSensor)
            {
                Sensor sensor = Read(newSensor.Id);
                if (sensor.Equals(Sensor.Null))
                {
                    newSensor.Id = Guid.NewGuid();
                    sensors.Add(newSensor);
                }
                else
                {
                    Update(sensor.Id, newSensor);                   
                }

                return newSensor.Id;
            }

            return Guid.Empty;
        }

        /// <summary>
        /// Delete sensor with the given id.
        /// </summary>
        /// <param name="id">Id of the wanted sensor.</param>
        /// <returns>True if deletion worked, False otherwise.</returns>
        public bool Delete(Guid id)
        {
            return 0 < sensors.RemoveAll(s => s.Id.Equals(id));
        }

        /// <summary>
        /// Get sensor with id <code>id</code>.
        /// </summary>
        /// <param name="id">Id of wanted sensor.</param>
        /// <returns>Found sensor.</returns>
        public Sensor Read(Guid id)
        {
            Sensor sensorToReturn = sensors.FirstOrDefault(s => s.Id.Equals(id));
            if (null == sensorToReturn)
            {
                sensorToReturn = Sensor.Null;
            }
            return sensorToReturn;
        }

        /// <summary>
        /// Update sensor with id <code>id</code> by using a new <code>Sensor</code> variable.
        /// <para>This method only update sensor. It doesn't add sensors.</para>
        /// <para>How should it works when the user reads a sensor with <code>Read</code> methods? Should it allow update on sensor?</para>
        /// </summary>
        /// <param name="id">Id of the sensor to update.</param>
        /// <param name="updatedSensor">New values for the sensor.</param>
        /// <returns>True if update works, false otherwise.</returns>
        public bool Update(Guid id, Sensor updatedSensor)
        {
            if(null != updatedSensor)
            {
                Sensor sensor = Read(id);
                if (!sensor.Equals(Sensor.Null))
                {
                    sensor.Data = updatedSensor.Data;
                    sensor.Name = updatedSensor.Name;                    
                    return true;
                }                
            }

            return false;
        }
    }
}
