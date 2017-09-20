namespace SensorView.WindowsApp.Models
{
    using System;
    using System.Collections.ObjectModel;

    using OxyPlot;
    using OxyPlot.Axes;

    using SensorView.Services;

    using Smart.ComponentModel;

    public sealed class SensorItem : NotificationObject
    {
        private double? temperature;

        private double? humidity;

        private DateTime time;

        public string DeviceId { get; }

        public double? Temperature
        {
            get => temperature;
            private set => SetProperty(ref temperature, value);
        }

        public double? Humidity
        {
            get => humidity;
            private set => SetProperty(ref humidity, value);
        }

        public DateTime Time
        {
            get => time;
            private set => SetProperty(ref time, value);
        }

        public ObservableCollection<DataPoint> Temperatures { get; } = new ObservableCollection<DataPoint>();

        public ObservableCollection<DataPoint> Humidities { get; } = new ObservableCollection<DataPoint>();

        /// <summary>
        ///
        /// </summary>
        /// <param name="deviceId"></param>
        public SensorItem(string deviceId)
        {
            DeviceId = deviceId;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Ignore")]
        public void Update(SensorValue value)
        {
            if (value.Temperature.HasValue)
            {
                if (Temperatures.Count > 72)
                {
                    Temperatures.RemoveAt(0);
                }
                Temperatures.Add(new DataPoint(Axis.ToDouble(value.Time), value.Temperature.Value));
            }

            if (value.Humidity.HasValue)
            {
                if (Humidities.Count > 72)
                {
                    Humidities.RemoveAt(0);
                }
                Humidities.Add(new DataPoint(Axis.ToDouble(value.Time), value.Humidity.Value));
            }

            Temperature = value.Temperature;
            Humidity = value.Humidity;
            Time = value.Time;
        }
    }
}
