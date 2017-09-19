namespace SensorView.WindowsApp.Models
{
    using System;
    using Smart.ComponentModel;

    public sealed class SensorItem : NotificationObject
    {
        private string deviceId;

        private double? temperture;

        private double? humidity;

        private DateTime time;

        public string DeviceId
        {
            get => deviceId;
            set => SetProperty(ref deviceId, value);
        }

        public double? Temperture
        {
            get => temperture;
            set => SetProperty(ref temperture, value);
        }

        public double? Humidity
        {
            get => humidity;
            set => SetProperty(ref humidity, value);
        }

        public DateTime Time
        {
            get => time;
            set => SetProperty(ref time, value);
        }
    }
}
