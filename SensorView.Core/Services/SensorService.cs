namespace SensorView.Services
{
    using System;
    using System.Reactive.Subjects;

    /// <summary>
    ///
    /// </summary>
    public class SensorService
    {
        private readonly Subject<SensorValue> valueStream = new Subject<SensorValue>();

        public IObservable<SensorValue> ValueStream => valueStream;

        /// <summary>
        ///
        /// </summary>
        public void Start()
        {
            // TODO
            valueStream.OnNext(new SensorValue { DeviceId = "000000000000" });
            valueStream.OnNext(new SensorValue { DeviceId = "444444444444" });
            valueStream.OnNext(new SensorValue { DeviceId = "222222222222" });
            valueStream.OnNext(new SensorValue { DeviceId = "555555555555" });
            valueStream.OnNext(new SensorValue { DeviceId = "111111111111" });
            valueStream.OnNext(new SensorValue { DeviceId = "333333333333" });
        }

        /// <summary>
        ///
        /// </summary>
        public void Stop()
        {
            // TODO
        }
    }
}
