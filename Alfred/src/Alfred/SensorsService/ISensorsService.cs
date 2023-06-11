using AlfredUtilities.Sensors;

using System;
using System.Collections.Generic;

namespace Alfred.SensorsService
{
    public interface ISensorsService : IDisposable
    {
        #region Public Properties

        IList<Sensor> Sensors
        {
            get;
        }

        #endregion Public Properties

        #region Public Methods

        Guid Add(Sensor newSensor);

        bool Delete(Guid id);

        Sensor Read(Guid id);

        bool Update(Guid id, Sensor updatedSensor);

        #endregion Public Methods
    }
}
