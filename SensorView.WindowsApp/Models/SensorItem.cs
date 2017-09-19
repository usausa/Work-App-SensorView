namespace SensorView.WindowsApp.Models
{
    using System;

    using SensorView.Services;

    using Smart.ComponentModel;

    public sealed class SensorItem : NotificationObject
    {
        private double? temperture;

        private double? humidity;

        private DateTime time;

        // TODO 時系列データ

        public string DeviceId { get; }

        public double? Temperture
        {
            get => temperture;
            private set => SetProperty(ref temperture, value);
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
            this.DeviceId = deviceId;
        }

        public void Update(SensorValue value)
        {
            Temperture = value.Temperture;
            Humidity = value.Humidity;
            Time = value.Time;
        }
    }
}
