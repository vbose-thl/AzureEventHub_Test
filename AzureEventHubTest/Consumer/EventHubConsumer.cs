using System;
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
            EhConnectionString = ConfigHelper.Configuration["Eh_Listen_ConnectionString"];
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

        private void StartEventProcessor(EventProcessorHost eventProcessorHost)
        {
            var options = new EventProcessorOptions();
            options.ReceiveTimeout = TimeSpan.FromMinutes(2);
            options.SetExceptionHandler((eventargs) => {
                Console.WriteLine(eventargs.Exception.Message);
            });

            eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>(options).Wait();
        }

        private void StopEventProcessor(EventProcessorHost eventProcessorHost)
        {
            eventProcessorHost.UnregisterEventProcessorAsync().Wait();
        }
        
        
        public void Start()
        {
            StartEventProcessor(_eventProcessorHost);
        }

        public void Stop()
        {
            StopEventProcessor(_eventProcessorHost);
        }
        
    }
}