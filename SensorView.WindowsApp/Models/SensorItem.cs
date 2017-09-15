namespace SensorView.WindowsApp.Models
{
    using Smart.ComponentModel;

    public sealed class SensorItem : NotificationObject
    {
        private string deviceId;

        public string DeviceId
        {
            get => deviceId;
            set => SetProperty(ref deviceId, value);
        }
    }
}
