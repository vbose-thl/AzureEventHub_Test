using System.Threading.Tasks;
using AzureEventHubTest.Consumer.EventProcessor;
using AzureEventHubTest.Helpers;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;

namespace AzureEventHubTest.Consumer
{
    public class EventHubConsumer
    {
        private EventProcessorHost _eventProcessorHost;

        public string StorageContainerName { get; set; }

        public string StorageConnectionString { get; set; }

        public string EhConnectionString { get; set; }

        public string EhEntityPath { get; set; }

        public EventHubConsumer()
        {
            StorageContainerName = ConfigHelper.Configuration["StorageContainerName"];
            StorageConnectionString = ConfigHelper.Configuration["StorageConnectionString"];
            EhConnectionString = ConfigHelper.Configuration["EhConnectionString"];
            EhEntityPath = ConfigHelper.Configuration["EhEntityPath"];

            _eventProcessorHost = CreateEventProcessor();
        }

        public EventProcessorHost CreateEventProcessor()
        {
            var eventProcessorHost = new EventProcessorHost(
                EhEntityPath,
                PartitionReceiver.DefaultConsumerGroupName,
                EhConnectionString,
                StorageConnectionString,
                StorageContainerName);

            return eventProcessorHost;
        }

        private async Task StartEventProcessor(EventProcessorHost eventProcessorHost)
        {
            await eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>();
        }

        private async Task StopEventProcessor(EventProcessorHost eventProcessorHost)
        {
            await eventProcessorHost.UnregisterEventProcessorAsync();
        }
        
        
        public async Task Start()
        {
            await StartEventProcessor(_eventProcessorHost);
        }

        public async Task Stop()
        {
            await StopEventProcessor(_eventProcessorHost);
        }
        
    }
}