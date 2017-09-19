namespace SensorView.Services
{
    using System;

    public class SensorValue
    {
        public string DeviceId { get; set; }

        public double? Temperture { get; set; }

        public double? Humidity { get; set; }

        public DateTime Time { get; set; }
    }
}
