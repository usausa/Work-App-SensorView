namespace SensorView.WindowsApp.Views
{
    using Smart.ComponentModel;

    public class SensorTileViewModel : NotificationObject
    {
        // TODO これは固定にするか？
        private string deviceId;

        public string DeviceId
        {
            get => deviceId;
            set => SetProperty(ref deviceId, value);
        }
    }
}
