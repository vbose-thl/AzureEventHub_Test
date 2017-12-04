using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
                    producer.SendMessage(messageJson).GetAwaiter().GetResult();

                }

                Console.WriteLine("Stopping Producer");

                producer.ShutdownSender();

//                Console.WriteLine("Press any key to receive message ..");
//
//                Console.ReadLine();
//                Console.Read();
//
//                var consumer = new EventHubConsumer();
//
//                Console.WriteLine("Starting Consumer");
//
//                consumer.Start();
//

                Console.WriteLine("Press any key to stop receiving message ..");
                
                Console.ReadLine();
                Console.Read();

                Console.WriteLine("Stopping Consumer");
                
                //consumer.Stop();

                Console.ReadLine();
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
                Vehicle = "TestVehicle123",
                DeviceId = "1234",
                EventId = "1234567",
                EventType = "Provisioning",
                EventTime = DateTimeOffset.Parse("2017-11-15T13:22:16Z"),
                Speed = "90 km/h",
                Address = "56-58 Esplanade Inner Kaiti Gisborne",
                Latitude = "-38.67038",
                Longitude =  "178.03108",
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