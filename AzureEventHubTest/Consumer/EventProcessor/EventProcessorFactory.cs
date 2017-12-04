using Microsoft.Azure.EventHubs.Processor;

namespace AzureEventHubTest.Consumer.EventProcessor
{
    public class EventProcessorFactory : IEventProcessorFactory
    {
        public EventProcessorFactory()
        {
        }

        public IEventProcessor CreateEventProcessor(PartitionContext context)
        {
            return new SimpleEventProcessor();
        }
    }
}