using System;
using AzureEventHubTest.Consumer;
using AzureEventHubTest.Producer;

namespace AzureEventHubTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                Console.WriteLine("Starting Producer");

                var producer = new EventHubProducer();

                Console.WriteLine("Sending Message");

                producer.SendMessage("Test Message").GetAwaiter().GetResult();

                Console.WriteLine("Stopping Producer");

                producer.ShutdownSender();

                Console.WriteLine("Press any key to receive message ..");

                Console.ReadLine();
                Console.Read();

                var consumer = new EventHubConsumer();

                Console.WriteLine("Starting Consumer");

                consumer.Start();


                Console.WriteLine("Press any key to stop receiving message ..");
                
                Console.ReadLine();
                Console.Read();

                Console.WriteLine("Stopping Consumer");
                
                consumer.Stop();

                Console.ReadLine();
                Console.Clear();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception {e.StackTrace}");
            }

        }
    }
}