using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AlfredFront.Data
{
    public class Sensor : INotifyPropertyChanged
    {
        private Guid _id;
        public Guid Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public List<SensorData> Data { get; set; } = new List<SensorData>();

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class SensorData : INotifyPropertyChanged
    {
        public string Name { get; set; }


        private object _value;
        /// <summary>
        /// Value of the data.
        /// </summary>
        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                NotifyPropertyChanged("Value");
            }
        }

        /// <summary>
        /// Type of the value.
        /// </summary>
        public Type Type
        {
            get { return Value.GetType(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
