namespace SensorView.Models
{
    using Smart.ComponentModel;

    using System.Collections.ObjectModel;

    public class SensorManager : NotificationObject
    {
        public ObservableCollection<SensorItem> Sensors { get; }

        public SensorManager()
        {
            Sensors = new ObservableCollection<SensorItem>
            {
                new SensorItem { DeviceId = "000000000001" },
                new SensorItem { DeviceId = "000000000002" },
                new SensorItem { DeviceId = "000000000003" },
                new SensorItem { DeviceId = "000000000004" },
                new SensorItem { DeviceId = "000000000005" },
                new SensorItem { DeviceId = "000000000006" },
                new SensorItem { DeviceId = "000000000007" },
                new SensorItem { DeviceId = "000000000008" },
                new SensorItem { DeviceId = "000000000009" },
                new SensorItem { DeviceId = "000000000010" },
                new SensorItem { DeviceId = "000000000011" },
                new SensorItem { DeviceId = "000000000012" }
            };
        }
    }
}
