using Azure.Messaging.ServiceBus;
using com.tweetapp.Repository;
using Microsoft.Extensions.Options;
using com.tweetapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace com.tweetapp.ServiceBus
{
    public class serviceBusSender
    {
        private readonly ServiceBusClient _client;
        private readonly ServiceBusSender _sender;
        private const string QUEUE_NAME="tweetapp";
        //private readonly ILogger<ServiceBusSender> _log;
        public serviceBusSender(IOptions<DbSettings> _Dbsettings)
        {
            var connectionstring = _Dbsettings.Value.azureSender;
            _client = new ServiceBusClient(connectionstring);
            _sender = _client.CreateSender(QUEUE_NAME);
        }

        public void sendMessage(tweet _tweet)
        {
            string tweet = JsonSerializer.Serialize(_tweet);
           // _log.LogInformation("sending Message:" + _tweet.Message + "by:" + _tweet.User_Name);
            ServiceBusMessage message = new ServiceBusMessage(tweet);
            _sender.SendMessageAsync(message).ConfigureAwait(false);
        }
    }
}
