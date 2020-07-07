using System;
using System.Collections.Generic;
using AlfredUtilities.Messages;
using AlfredUtilities.Sensors;

namespace Alfred.Sensors
{
    public interface ISensorService : IDisposable
    {
        IList<Sensor> Sensors
        {
            get;
        }

        Guid Add(Sensor newSensor);

        bool Update(Guid id, Sensor updatedSensor);

        Sensor Read(Guid id);

        bool Delete(Guid id);
    }
}