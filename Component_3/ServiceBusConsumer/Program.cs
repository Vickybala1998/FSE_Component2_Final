using Azure.Messaging.ServiceBus;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace tweetServiceBus_Consumer
{
    class Program
    {

        //private static ServiceBusClient _client;
        //private static ServiceBusProcessor _processor;
        private const string QUEUE_NAME = "tweetapp";
        private static IQueueClient _client;
        private const string CONNECTION_STRING = "Endpoint=sb://tweetappproducer1.servicebus.windows.net/;SharedAccessKeyName=Listner;SharedAccessKey=bnErI0QTvv/N6ow/szgGmNTWRpmQhrpSYsPl0D0f/54=;";
        static void Main(string[] args)
        {
            register();
        }

        public static void register()
        {
            _client = new QueueClient(CONNECTION_STRING, QUEUE_NAME);
            var _serviceBusProcessOptions = new MessageHandlerOptions(ExceptionMethod)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            _client.RegisterMessageHandler(ExecuteMessageProcessing, _serviceBusProcessOptions);
            Console.WriteLine("started");
            Console.Read();
        }

        private static async Task ExecuteMessageProcessing(Message message, CancellationToken arg2)
        {
             var result = JsonConvert.DeserializeObject<tweet>(Encoding.UTF8.GetString(message.Body));
            Console.WriteLine("message "+result.Message+"is send by"+result.User_Name);
            await _client.CompleteAsync(message.SystemProperties.LockToken);
        }
        private static async Task ExceptionMethod(ExceptionReceivedEventArgs arg)
        {
            await Task.Run(() =>
           Console.WriteLine($"Error occured. Error is {arg.Exception.Message}")
           );
        }

    }
}
