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

        private async Task SendMessage(Message msg)
        {
            if (client.IsConnected())
            {
                string json = JsonSerializer.Serialize(msg);
                await client.PublishAsync(Config.TOPIC, json).ConfigureAwait(false);
            }
        }

        // IProtocol Implementation

        public string[] GetOnlineMediatheque()
        {
            var request = new Message
            {
                Sender = MyName,
                Action = "askOnline",
                Recipient = "0.0.0.0"
            };
            SendMessage(request).Wait(); 
            return new string[] { }; 
        }

        public void SayOnline()
        {
            var message = new Message
            {
                Sender = MyName,
                Action = "online",
                Recipient = "0.0.0.0"
            };
            SendMessage(message).FireAndForgetSafeAsync();
        }

        public List<ISong> AskCatalog(string name)
        {
            var request = new Message
            {
                Sender = MyName,
                Recipient = name,
                Action = "askCatalog" 
            };
            SendMessage(request).FireAndForgetSafeAsync();
            return new List<ISong>();
        }

        public void SendCatalog(string name)
        {
             var message = new Message
             {
                 Sender = MyName,
                 Recipient = name,
                 Action = "sendCatalog",
                 SongList = P_BitRuisseau_321.Program.MySongs
             };
             SendMessage(message).FireAndForgetSafeAsync();
        }

        public void AskMedia(ISong song, string name, int startByte, int endByte)
        {
            var message = new Message
            {
                Sender = MyName,
                Recipient = name,
                Action = "askMedia",
                Hash = song.Hash,
                StartByte = startByte,
                EndByte = endByte
            };
            SendMessage(message).FireAndForgetSafeAsync();
        }

        public void SendMedia(ISong song, string name, int startByte, int endByte)
        {
             if (song is Song localSong)
            {
                try {
                    using (var fs = new FileStream(localSong.FilePath, FileMode.Open, FileAccess.Read))
                    {
                        int length = endByte - startByte + 1;
                        byte[] buffer = new byte[length];
                        fs.Seek(startByte, SeekOrigin.Begin);
                        fs.Read(buffer, 0, length);
                        
                        var message = new Message
                        {
                            Sender = MyName,
                            Recipient = name,
                            Action = "sendMedia",
                            Hash = song.Hash,
                            StartByte = startByte,
                            EndByte = endByte,
                            SongData = Convert.ToBase64String(buffer)
                        };
                        SendMessage(message).FireAndForgetSafeAsync();
                    }
                } catch(Exception ex) {
                    Trace.WriteLine("Error sending media: " + ex.Message);
                }
            }
        }
    }
    
    public static class TaskExtensions
    {
        public static async void FireAndForgetSafeAsync(this Task task)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }
    }
}
