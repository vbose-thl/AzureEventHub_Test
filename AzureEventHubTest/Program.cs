using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using AzureEventHubTest.Consumer;
using AzureEventHubTest.Producer;
using Newtonsoft.Json;

namespace AzureEventHubTest
{
    class Program
    {
        private static IList<TelemetricMessage> _messages = new List<TelemetricMessage>();

        static void Main(string[] args)
        {
            SetupTestMessages();

            try
            {
                Console.WriteLine("Starting Producer");

                var producer = new EventHubProducer();

                Console.WriteLine("Sending Message");

                foreach (var message in _messages)
                {
                    var messageJson = JsonConvert.SerializeObject(message);
                    var json = "[" + HttpUtility.JavaScriptStringEncode(messageJson) + "]";
                    producer.SendMessage(messageJson).Wait();
                }

                Console.WriteLine("Stopping Producer");

                producer.ShutdownSender();

                Console.ReadLine();
                
                //Uncomment the line below to test reading the sent messages
                
//                Console.WriteLine("Press any key to receive message ..");
//
//                Console.Read();
//
//                var consumer = new EventHubConsumer();
//
//                Console.WriteLine("Starting Consumer");
//
//                consumer.Start();
//
//                Console.ReadLine();
//
//                Console.WriteLine("Stopping Consumer");
//
//                consumer.Stop();

                Console.Read();

                Console.Clear();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception {e.StackTrace}");
            }
        }

        private static void SetupTestMessages()
        {
            _messages.Add(new TelemetricMessage()
            {
                Vehicle = "TestVehicle33223",
                DeviceId = "1234",
                EventId = "1234555",
                EventType = "Provisioning",
                EventTime = DateTimeOffset.Parse("2017-11-15T13:22:16Z"),
                Speed = "90 km/h",
                Address = "56-58 Esplanade Inner Kaiti Gisborne",
                Latitude = "-38.757038",
                Longitude = "168.03108",
                Odometer = "208330.0 km",
                Company = "MighWay",
            });
        }
    }

    public class TelemetricMessage
    {
        public string Vehicle { get; set; }

        public string DeviceId { get; set; }

        public string EventId { get; set; }

        public string EventType { get; set; }

        public DateTimeOffset EventTime { get; set; }

        public string Speed { get; set; }

        public string Address { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string Odometer { get; set; }

        public string Company { get; set; }
    }
}