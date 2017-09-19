namespace SensorView.WindowsApp.Models
{
    using System;

    using SensorView.Services;

    using Smart.ComponentModel;

    public sealed class SensorItem : NotificationObject
    {
        private double? temperature;

        private double? humidity;

        private DateTime time;

        // TODO 時系列データ

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

        public SensorItem(string deviceId)
        {
            DeviceId = deviceId;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Ignore")]
        public void Update(SensorValue value)
        {
            Temperature = value.Temperature;
            Humidity = value.Humidity;
            Time = value.Time;
        }
    }
}
