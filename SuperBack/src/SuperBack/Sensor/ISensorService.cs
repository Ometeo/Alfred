using System;
using System.Collections.Generic;

namespace SuperBack.Sensor
{
    public interface ISensorService : IDisposable
    {
        IList<Sensor> Sensors { get;  }

        Guid Add(Sensor newSensor);
        bool Update(Guid id, Sensor updatedSensor);
        Sensor Read(Guid id);
        bool Delete(Guid id);
    }
}
