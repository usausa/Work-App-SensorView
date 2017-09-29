namespace SensorView.Services
{
    using System;
    using System.Reactive.Subjects;
    using System.Text;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    using uPLibrary.Networking.M2Mqtt;
    using uPLibrary.Networking.M2Mqtt.Exceptions;
    using uPLibrary.Networking.M2Mqtt.Messages;

    /// <summary>
    ///
    /// </summary>
    public class SensorService
    {
        private readonly JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        private readonly string host;

        private readonly string clientId;

        private readonly string topic;

        private readonly Subject<SensorValue> valueStream = new Subject<SensorValue>();

        private readonly Subject<bool> connectionStream = new Subject<bool>();

        private MqttClient client;

        /// <summary>
        ///
        /// </summary>
        public IObservable<SensorValue> ValueStream => valueStream;

        /// <summary>
        ///
        /// </summary>
        public IObservable<bool> ConnectionStream => connectionStream;

        /// <summary>
        ///
        /// </summary>
        /// <param name="host"></param>
        /// <param name="clientId"></param>
        /// <param name="topic"></param>
        public SensorService(string host, string clientId, string topic)
        {
            this.host = host;
            this.clientId = clientId;
            this.topic = topic;
        }

        /// <summary>
        ///
        /// </summary>
        public void Start()
        {
            try
            {
                client = new MqttClient(host);
                client.MqttMsgPublishReceived += OnMqttMsgPublishReceived;
                client.ConnectionClosed += (sender, args) => { connectionStream.OnNext(false); };

                var ret = client.Connect(clientId);
                if (ret == 0)
                {
                    connectionStream.OnNext(true);
                    client.Subscribe(new[] { topic }, new[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                    return;
                }
            }
            catch (MqttConnectionException e)
            {
                System.Diagnostics.Trace.WriteLine(e);
            }
            catch (AggregateException e)
            {
                System.Diagnostics.Trace.WriteLine(e);
            }

            connectionStream.OnNext(false);
        }

        /// <summary>
        ///
        /// </summary>
        public void Stop()
        {
            if (client?.IsConnected ?? false)
            {
                client.Disconnect();
                client = null;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="o"></param>
        /// <param name="ea"></param>
        private void OnMqttMsgPublishReceived(object o, MqttMsgPublishEventArgs ea)
        {
            var json = Encoding.UTF8.GetString(ea.Message);
            var value = JsonConvert.DeserializeObject<SensorValue>(json, settings);
            if (!String.IsNullOrEmpty(value.DeviceId) && (value.Temperature.HasValue || value.Humidity.HasValue))
            {
                valueStream.OnNext(value);
            }
        }
    }
}
