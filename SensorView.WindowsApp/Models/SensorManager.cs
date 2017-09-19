namespace SensorView.WindowsApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Windows.Threading;

    using SensorView.Services;

    using Smart.ComponentModel;

    /// <summary>
    ///
    /// </summary>
    public sealed class SensorManager : NotificationObject
    {
        private readonly SerialDisposable subscription = new SerialDisposable();

        private readonly SensorService sensorService;

        private readonly IDictionary<string, SensorItem> sensorsByDeviceId = new Dictionary<string, SensorItem>();

        private bool enable;

        /// <summary>
        ///
        /// </summary>
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
        public ObservableCollection<SensorItem> Sensors { get; } = new ObservableCollection<SensorItem>();

        /// <summary>
        ///
        /// </summary>
        /// <param name="sensorService"></param>
        public SensorManager(SensorService sensorService)
        {
            this.sensorService = sensorService;

            subscription.Disposable = sensorService.ValueStream
                .ObserveOn(Dispatcher.CurrentDispatcher)
                .Subscribe(OnSensorValue);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        private void OnSensorValue(SensorValue value)
        {
            if (!sensorsByDeviceId.TryGetValue(value.DeviceId, out var item))
            {
                item = new SensorItem { DeviceId = value.DeviceId };
                sensorsByDeviceId[value.DeviceId] = item;
                var index = Sensors
                    .TakeWhile(x => String.Compare(x.DeviceId, value.DeviceId, StringComparison.OrdinalIgnoreCase) < 0)
                    .Count();
                Sensors.Insert(index, item);
            }

            item.Temperture = value.Temperture;
            item.Humidity = value.Humidity;
            item.Time = value.Time;
        }
    }
}
