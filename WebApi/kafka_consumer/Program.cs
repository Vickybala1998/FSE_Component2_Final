using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace tweet_Consumer_kafka
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConsumerConfig()
            {
                BootstrapServers = "localhost:9092",
                GroupId = "tweet_kafka",
                EnableAutoCommit = false
               
            };

            CancellationTokenSource canceltoken = new CancellationTokenSource();
            Console.CancelKeyPress +=(_,e) => {
                canceltoken.Cancel();
            };

            const string topic = "tweet";
            using (var consumer = new ConsumerBuilder<string, string>(config).Build())
            {
                consumer.Assign(new TopicPartition(topic, new Partition()));
                while (true)
                {
                    var cr = consumer.Consume(canceltoken.Token);
                    Console.WriteLine($"Consumed event from topic {topic} with key {cr.Message.Key} and value {cr.Message.Value}");
                    saveToDb(cr.Message.Key, cr.Message.Value);
                }

            }
        }
        public static  void saveToDb(string username, string message)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:39975/api/v1.0/tweets/addTweet");
            request.Content = new StringContent("{\"User_Name\":\"" + username + "\",\"Message\":\"" + message + "\"}", Encoding.UTF8, "application/json");
            client.SendAsync(request);
        }
    }
}
