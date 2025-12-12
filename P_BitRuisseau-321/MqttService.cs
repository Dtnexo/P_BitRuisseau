using HiveMQtt.Client;
using HiveMQtt.Client.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Configuration;
using System.Security;
using System.Linq;

namespace BitRuisseau
{
    public class MqttService : IProtocol
    {
        private HiveMQClient client;
        private string MyName = Environment.MachineName;

        public MqttService()
        {
            var host = ConfigurationManager.AppSettings["host"] ?? "test.mosquitto.org";
            var username = ConfigurationManager.AppSettings["username"];
            var passwordString = ConfigurationManager.AppSettings["password"] ?? "";
            
            var securePassword = new SecureString();
            foreach (char c in passwordString) securePassword.AppendChar(c);

            var options = new HiveMQClientOptions
            {
                Host = host, 
                Port = 1883,
                ClientId = MyName + "_" + Guid.NewGuid().ToString().Substring(0, 5),
                UserName = username,
                Password = securePassword
            };
            
            client = new HiveMQClient(options);
            Connect();
        }

        public async void Connect()
        {
            try
            {
                await client.ConnectAsync().ConfigureAwait(false);
                Trace.WriteLine("Connected to MQTT Broker");

                client.OnMessageReceived += (sender, args) =>
                {
                    try
                    {
                        string payload = args.PublishMessage.PayloadAsString;
                        Trace.WriteLine($"Message Received: {payload}");
                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var message = JsonSerializer.Deserialize<Message>(payload, options);
                        if (message != null)
                        {
                            HandleMessage.Handle(message, this);
                        }
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine($"Error handling message: {e.Message}");
                    }
                };

                await client.SubscribeAsync(Config.TOPIC).ConfigureAwait(false);
                Trace.WriteLine($"Subscribed to {Config.TOPIC}");
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"MQTT Connect Error: {ex.Message}");
            }
        }

}
