using Microsoft.Extensions.Configuration;
using com.tweetapp.Repository;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using com.tweetapp.Models;
using com.tweetapp.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.ServiceBus;
using System.Threading;

namespace com.tweetapp.ServiceBus
{
    public class serviceBusConsumer : IServiceConsumer
    {
        private readonly IQueueClient _client;
        private ServiceBusProcessor _processor;
        private const string QUEUE_NAME = "tweetapp";
        private ITweetService _tweetService;
        private ILogger<serviceBusConsumer> _log;
        public serviceBusConsumer(IOptions<DbSettings> _DbSettings, ITweetService tweetService, ILogger<serviceBusConsumer> log)
        {
            var connectionstring = _DbSettings.Value.azureReceiver;
            _client = new QueueClient(connectionstring, QUEUE_NAME);
            _tweetService = tweetService;
            _log = log;
        }

        public void register()
        {
            var _serviceBusProcessOptions = new MessageHandlerOptions(ExceptionMethod)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            _client.RegisterMessageHandler(ExecuteMessageProcessing, _serviceBusProcessOptions);
            Console.Read();
        }
        private async Task ExecuteMessageProcessing(Message message, CancellationToken arg2)
        {
            // var result = JsonConvert.DeserializeObject<OrderInformation>(Encoding.UTF8.GetString(message.Body));
            //Console.WriteLine($"Order Id is {result.OrderId}, Order name is {result.OrderName} and quantity is {result.OrderQuantity}");
            Console.WriteLine("starting");
            await _client.CompleteAsync(message.SystemProperties.LockToken);
        }
        private async Task ExceptionMethod(ExceptionReceivedEventArgs arg)
        {
            await Task.Run(() =>
           Console.WriteLine($"Error occured. Error is {arg.Exception.Message}")
           );
        }

    }
}
