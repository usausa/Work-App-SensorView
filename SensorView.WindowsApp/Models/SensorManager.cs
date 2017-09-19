namespace SensorView.WindowsApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;

    using SensorView.Services;

    using Smart.ComponentModel;

    /// <summary>
    ///
    /// </summary>
    public sealed class SensorManager : NotificationObject, IDisposable
    {
        private readonly CompositeDisposable subscriptions = new CompositeDisposable();

        private readonly SerialDisposable connection = new SerialDisposable();

        private readonly SensorService sensorService;

        private readonly IDictionary<string, SensorItem> sensorsByDeviceId = new Dictionary<string, SensorItem>();

        private bool enable;

        private bool connected;

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
                        connection.Disposable = sensorService.ConnectionStream
                            .TakeWhile(x => !x)
                            .Repeat()
                            .SelectMany(x => Observable.Timer(TimeSpan.FromSeconds(5)))
                            .Subscribe(x =>
                            {
                                sensorService.Start();
                            });
                        sensorService.Start();
                    }
                    else
                    {
                        connection.Disposable = null;
                        sensorService.Stop();
                    }
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public bool Connected
        {
            get => connected;
            set => SetProperty(ref connected, value);
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
            this.sensorService = sensorService ?? throw new ArgumentNullException(nameof(sensorService));

            subscriptions.Add(sensorService.ConnectionStream
                .ObserveOnDispatcher()
                .Subscribe(x => Connected = x));
            subscriptions.Add(sensorService.ValueStream
                .ObserveOnDispatcher()
                .Subscribe(OnSensorValue));
        }

        /// <summary>
        ///
        /// </summary>
        public void Dispose()
        {
            connection.Dispose();
            subscriptions.Dispose();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        private void OnSensorValue(SensorValue value)
        {
            if (!sensorsByDeviceId.TryGetValue(value.DeviceId, out var item))
            {
                item = new SensorItem(value.DeviceId);
                sensorsByDeviceId[value.DeviceId] = item;
                var index = Sensors
                    .TakeWhile(x => String.Compare(x.DeviceId, value.DeviceId, StringComparison.OrdinalIgnoreCase) < 0)
                    .Count();
                Sensors.Insert(index, item);
            }

            item.Update(value);
        }
    }
}
