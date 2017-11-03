using System;
using System.Text;
using System.Threading.Tasks;
using AzureEventHubTest.Helpers;
using Microsoft.Azure.EventHubs;

namespace AzureEventHubTest.Producer
{
    public class EventHubProducer
    {
        private EventHubClient _eventHubClient;
        
        public string EhEntityPath { get; set; }

        public string EhConnectionString { get; set; }

        public EventHubProducer()
        {
            EhConnectionString = ConfigHelper.Configuration["EhConnectionString"];
            EhEntityPath = ConfigHelper.Configuration["EhEntityPath"];
            _eventHubClient = CreateEventHubClient();
        }

        public EventHubClient CreateEventHubClient()
        {
            var connectionStringBuilder = new EventHubsConnectionStringBuilder(EhConnectionString)
            {
                EntityPath = EhEntityPath
            };

            var eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());

            return eventHubClient;
        }

        public async Task SendMessage(string message)
        {
            try
            {
                Console.WriteLine($"Sending message: {message}");
                await _eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(message)));
                Console.WriteLine("Message sent!! ");

            }
            catch (Exception exception)
            {
                Console.WriteLine($"Exception in sending message: {exception.Message}");
            }
        }

        public void ShutdownSender()
        {
            _eventHubClient.CloseAsync().GetAwaiter().GetResult();
        }

    }
}