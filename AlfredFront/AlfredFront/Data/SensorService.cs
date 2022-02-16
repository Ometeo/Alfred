using System.Collections.ObjectModel;

namespace AlfredFront.Data
{
    public class SensorService
    {
        public ObservableCollection<Sensor> Sensors { get; set; } = new ObservableCollection<Sensor>();

        public EventHandler SensorsUdpated;

        public void Update(Sensor sensor)
        {
            Sensor s = Sensors.FirstOrDefault(x => x.Id == sensor.Id);
            if (s == null)
            {
                Sensors.Add(sensor);
            }
            else
            {
                s.Name = sensor.Name;
                for (int index = 0; index < sensor.Data.Count; ++index)
                {
                    s.Data[index].Value = sensor.Data[index].Value;
                }
            }

            SensorsUdpated.Invoke(this, EventArgs.Empty);
        }
    }
}
