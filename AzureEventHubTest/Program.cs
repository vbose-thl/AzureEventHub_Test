using System;
using AzureEventHubTest.Consumer;
using AzureEventHubTest.Producer;

namespace AzureEventHubTest
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Starting Producer");

            var producer = new EventHubProducer();
            
            Console.WriteLine("Sending Message");

            producer.SendMessage("Test Message").GetAwaiter().GetResult();

            Console.WriteLine("Stopping Producer");
            
            producer.ShutdownSender();
            
            Console.WriteLine("Press any key to receive message ..");

            Console.Read();

            var consumer = new EventHubConsumer();

            Console.WriteLine("Starting Consumer");

            consumer.Start().GetAwaiter().GetResult();
            
            Console.WriteLine("Stopping Consumer");

            consumer.Stop().GetAwaiter().GetResult();

            Console.Read();

        }
    }
}