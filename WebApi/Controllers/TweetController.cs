using com.tweetapp.Models;
using com.tweetapp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace com.tweetapp.Controllers
{
    [Route("api/v1.0/tweets")]
    [ApiController]
    public class TweetController : Controller
    {
        private readonly ITweetService _service;
        private readonly ILogger<TweetController> _log;

        public TweetController(ITweetService Service,ILogger<TweetController> Log)
        {
            _service = Service;
            _log = Log;
        }

        [HttpGet("all")]
        public IActionResult getAllTweets()
        {
            try
            {
                _log.LogInformation("get All Tweets started");
                List<tweet> AllTweets = _service.getAllTweets();
                _log.LogInformation("get All Tweets Completed");
                return Ok(AllTweets);
            }
            catch (Exception e)
            {
                _log.LogError(e.Message.ToString());
                return StatusCode(500);
            }
        }

        [HttpGet("{username}")]
        public IActionResult getTweetByUserId(string username)
        {
            try
            {
                if(!username.Contains('@'))
                {
                    username = '@' + username;
                }
                List<tweet> Tweets = _service.getTweetByUserId(username);

                return Ok(Tweets);
                
            }
            catch (Exception e)
            {
                _log.LogError(e.Message.ToString());
                return StatusCode(500);
            }
        }

        [HttpPost("{username}/add")]
        public IActionResult postTweet([FromBody] tweet _tweet,string username)
        {
            try
            {
                _tweet.User_Name = username;
                var config = new ProducerConfig()
                {
                    BootstrapServers = "localhost:9092"
                };
                const string topic = "tweet";

                using (var producer = new ProducerBuilder<string, string>(config).Build())
                {
                    producer.Produce(topic, new Message<string, string>{ Key = _tweet.User_Name, Value = _tweet.Message });
                    producer.Flush(TimeSpan.FromSeconds(10));
                }
               return StatusCode(200);
                
            }
            catch (Exception e)
            {
                _log.LogError(e.Message.ToString());
                return StatusCode(500);
            }
        }
        [HttpPost("addTweet")]
        public IActionResult addTweet([FromBody]tweet _tweet)
        {
            if (_service.postTweet(_tweet))
            {
                return StatusCode(200);
            }
            else
            {
                return StatusCode(400);
            }
        }

        [HttpPut("{username}/update/{id}")]
        public IActionResult updateTweet([FromBody] tweet _tweet,Guid id)
        {
            try
            {
                _tweet.Id = id;
                if (_service.updateTweet(_tweet))
                {
                    return StatusCode(200);
                }
                else
                {
                    return StatusCode(400);
                }
            }
            catch (Exception e)
            {
                _log.LogError(e.Message.ToString());
                return StatusCode(500);
            }
        }

        [HttpDelete("{username}/delete/{id}")]
        public IActionResult deleteTweet(Guid id)
        {
            try
            {
                if (_service.deleteTweet(id))
                {
                    return StatusCode(200);
                }
                else
                {
                    return StatusCode(400);
                }
            }
            catch (Exception e)
            {
                _log.LogError(e.Message.ToString());
                return StatusCode(500);
            }
        }

        [HttpPut("{username}/like/{id}")]
        public IActionResult likeTweet(Guid id,string like)
        {
            try
            {
                _service.likeTweet(id,like);
                return StatusCode(200);
            }
            catch(Exception e)
            {
                _log.LogError(e.Message.ToString());
                return StatusCode(500);
            }
        }
        [HttpPut("{username}/reply/{id}")]
        public IActionResult replyToTweet([FromBody] tweet _tweet,string username,Guid id)
        {
            try
            {
                _tweet.User_Name = username;
                _tweet.Id = id;
                _service.replyToTweet(_tweet);
                return StatusCode(200);
            }
            catch(Exception e)
            {
                _log.LogError(e.Message.ToString());
                return StatusCode(500);
            }
        }
    }
}
