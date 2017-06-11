using System;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using System.Threading.Tasks;

namespace ReceiveEventHub
{
    class Program
    {
        private const string EhConnectionString = "Endpoint=sb://sampleeventhubns.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=mn/Fc2siWV7w7tIo304F7Ml1AWTGI6lq4gR/mmwlQWY=";
        private const string EhEntityPath = "eventhubashish";
        private const string StorageContainerName = "containerashish";
        private const string StorageAccountName = "storageaccountashish";
        private const string StorageAccountKey = "77srUrZS7w410lFj/MViCouo7sCHuLXhCbDlMiRBmcwhmoP1GEAfNhE3+Rw0cuHvxWPzwzi+D+5jU01LyiEJng==";

        private static readonly string StorageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", StorageAccountName, StorageAccountKey);

                static void Main(string[] args)
                {
                    MainAsync(args).GetAwaiter().GetResult();
                }
                private static async Task MainAsync(string[] args)
                {
                    Console.WriteLine("Registering EventProcessor...");

                    var eventProcessorHost = new EventProcessorHost(
                        EhEntityPath,
                        PartitionReceiver.DefaultConsumerGroupName,
                        EhConnectionString,
                        StorageConnectionString,
                        StorageContainerName);

                    // Registers the Event Processor Host and starts receiving messages
                    await eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>();

                    Console.WriteLine("Receiving. Press ENTER to stop worker.");
                    Console.ReadLine();

                    // Disposes of the Event Processor Host
                    await eventProcessorHost.UnregisterEventProcessorAsync();
                }
    }

}
