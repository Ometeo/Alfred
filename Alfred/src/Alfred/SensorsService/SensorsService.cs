﻿using AlfredUtilities;
using AlfredUtilities.Messages;
using AlfredUtilities.Sensors;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Alfred.SensorsService
{
    /// <summary>
    /// Simple implementation of the Sensor Service Interface.
    ///
    /// <para>Use GUID for attributing Id's of sensors.</para>
    ///
    /// </summary>
    public class SensorsService : AlfredBase, ISensorsService, IMessageListener
    {
        #region Private Fields

        private readonly IMessageDispatcher _dispatcher;
        private readonly ILogger _logger;
        private readonly List<Sensor> sensors = new();

        #endregion Private Fields

        #region Public Constructors

        public SensorsService(IMessageDispatcher dispatcher, ILoggerFactory loggerFactory)
        {
            ArgumentNullException.ThrowIfNull(dispatcher);
            ArgumentNullException.ThrowIfNull(loggerFactory);

            _logger = loggerFactory.CreateLogger<SensorsService>();
            _dispatcher = dispatcher;
            bool registerResult = dispatcher.Register("NewSensor", this);
            registerResult &= dispatcher.Register("UpdateSensor", this);
            registerResult &= dispatcher.Register("ReadSensor", this);

            if (registerResult)
            {
                // todo log successful register.
            }
        }

        #endregion Public Constructors

        #region Public Properties

        public IList<Sensor> Sensors => sensors;

        #endregion Public Properties

        #region Public Methods

        // Todo make it read-only, sensors should be updated only by interface methods. => frozen collection?
        /// <summary>
        /// Add a sensor to the sensors list.
        ///
        /// <para>If the list already contains a sensor with the same id it is replaced by the new one (update).</para>
        /// </summary>
        /// <param name="newSensor">Sensor to add.</param>
        /// <returns>The new of the added sensor. <see>Guid.Empty</see> if <code>newSensor</code> is <code>null</code></returns>
        public Guid Add(Sensor newSensor)
        {
            ArgumentNullException.ThrowIfNull(newSensor);
            if (!newSensor.Equals(Sensor.Null))
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

        public void Consume(Message message)
        {
            ArgumentNullException.ThrowIfNull(message);
            if (message.Topic == "NewSensor")
            {
                Guid id = Add(message.Content as Sensor ?? Sensor.Null);
                Message newMessage = new()
                {
                    Topic = "NewSensorResponse",
                    Content = id
                };

                _dispatcher.EnqueueMessage(newMessage);
                _dispatcher.DequeueMessage();
            }

            if (message.Topic == "ReadSensor")
            {
                Sensor sensor = Read((Guid)(message?.Content ?? Guid.Empty));
                Message newMessage = new()
                {
                    Topic = "ReadSensorResponse",
                    Content = sensor
                };

                _dispatcher.EnqueueMessage(newMessage);
                _dispatcher.DequeueMessage();
            }
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
            Sensor sensorToReturn = sensors.Find(s => s.Id.Equals(id)) ?? Sensor.Null;
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
            ArgumentNullException.ThrowIfNull(updatedSensor);

            Sensor sensor = Read(id);
            if (!sensor.Equals(Sensor.Null))
            {
                sensor.Data = updatedSensor.Data;
                sensor.Name = updatedSensor.Name;
                return true;
            }

            return false;
        }

        #endregion Public Methods

        #region Protected Methods

        protected override void DisposeManagedObjects()
        {
            _logger.LogInformation("    * Dispose Managed Objects in SensorService");
            foreach (Sensor sensor in Sensors)
            {
                sensor.Dispose();
            }
        }

        protected override void DisposeUnmanagedObjects()
        {
            _logger.LogInformation("    * Dispose Unmanaged Objects in SensorService");
        }

        #endregion Protected Methods
    }
}
