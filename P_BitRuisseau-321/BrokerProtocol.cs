using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace BitRuisseau
{
    public class BrokerProtocol : IProtocol
    {
        private const string BrokerAddress = "127.0.0.1";
        private const int BrokerPort = 5000;
        private readonly string MyName = Environment.MachineName;

        private void SendMessageToBroker(Message message)
        {
            try
            {
                using (var client = new TcpClient(BrokerAddress, BrokerPort))
                using (var stream = client.GetStream())
                {
                    string jsonMessage = JsonSerializer.Serialize(message);
                    byte[] data = Encoding.UTF8.GetBytes(jsonMessage);

                    byte[] lengthPrefix = BitConverter.GetBytes(data.Length);
                    stream.Write(lengthPrefix, 0, lengthPrefix.Length);
                    stream.Write(data, 0, data.Length);
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Erreur de connexion au broker: {ex.Message}");
            }
        }

        public void SayOnline()
        {
            var message = new Message
            {
                Sender = MyName,
                Action = "IM_ONLINE"
            };
            SendMessageToBroker(message);
        }

