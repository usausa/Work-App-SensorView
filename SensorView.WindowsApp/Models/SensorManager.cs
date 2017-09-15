namespace SensorView.WindowsApp.Models
{
    using System.Collections.ObjectModel;

    using SensorView.Services;

    using Smart.ComponentModel;

    /// <summary>
    ///
    /// </summary>
    public sealed class SensorManager : NotificationObject
    {
        private readonly SensorService sensorService;

        private bool enable;

        public bool Enable
        {
            get => enable;
            set
            {
                if (SetProperty(ref enable, value))
                {
                    if (value)
                    {
                        sensorService.Start();
                    }
                    else
                    {
                        sensorService.Stop();
                    }
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public ObservableCollection<SensorItem> Sensors { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sensorService"></param>
        public SensorManager(SensorService sensorService)
        {
            this.sensorService = sensorService;

            // TODO
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
