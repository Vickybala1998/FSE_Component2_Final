using com.tweetapp.Models;
using com.tweetapp.Repository;
using com.tweetapp.Services;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetAppNunit
{
    [TestFixture]
    class TweetUnitTest
    {
        private IDbClient _client = new TestDbClient();
        private tweetService _tweetService;
        private IMongoCollection<tweet> _tweets;
        private readonly ILogger<tweetService> _log;
        public TweetUnitTest()
        {
            _tweetService = new tweetService(_client,_log);
            _tweets = _client.getTweetsCollection();
        }
        private tweet GetTweet(tweet tweet) => _tweets.Find(t => t.Id == tweet.Id).FirstOrDefault();
        [Test]
        public void PostTweetTest()
        {
            tweet tweet = new tweet { Message = "Test", User_Name = "test", Posted_On = DateTime.Now.ToString() };
            bool add = _tweetService.postTweet(tweet);
            Assert.IsTrue(add);
            RemoveTweet(tweet);
        }
        [Test]
        public void GetTweetsTest()
        {
            tweet tweet = new tweet { Message = "Test", User_Name = "test", Posted_On = DateTime.Now.ToString() };
            _tweetService.postTweet(tweet);
            var tweets = _tweetService.getAllTweets();
            Assert.IsTrue(tweets.Count > 0);
            RemoveTweet(tweet);
        }
        [Test]
        public void UpdateTweetTest()
        {
            tweet tweet = new tweet { Message = "Test", User_Name = "test", Posted_On = DateTime.Now.ToString() };
            _tweetService.postTweet(tweet);
            var filter = Builders<tweet>.Filter.Eq("User_Name", tweet.User_Name);
            tweet tweet1 = _tweets.Find(filter).First();
            tweet1.Message = "Working";
            _tweetService.updateTweet(tweet1);
            tweet tweet2 = _tweets.Find(filter).First();
            Assert.AreEqual(tweet1.Id, tweet2.Id);
            RemoveTweet(tweet2);
        }
 
        [Test]
        public void DeleteTweetTest()
        {
            tweet tweet = new tweet { Message = "Test", User_Name = "test", Posted_On = DateTime.Now.ToString() };
            _tweetService.postTweet(tweet);
            var filter = Builders<tweet>.Filter.Eq("User_Name", tweet.User_Name);
            tweet tweet1 = _tweets.Find(filter).FirstOrDefault();
            _tweetService.deleteTweet(tweet1.Id);
            tweet tweet2 = GetTweet(tweet1);
            Assert.AreEqual(tweet2, null);
        }
        [Test]
        public void LikeTweetTest()
        {
            tweet tweet = new tweet { Message = "Test", User_Name = "test", Posted_On = DateTime.Now.ToString() };
            _tweetService.postTweet(tweet);
            var filter = Builders<tweet>.Filter.Eq("User_Name", tweet.User_Name);
            tweet tweet1 = _tweets.Find(filter).FirstOrDefault();
            _tweetService.likeTweet(tweet1.Id,"1");
            tweet tweet2 = _tweets.Find(filter).FirstOrDefault();
            Assert.AreEqual(tweet1.No_of_Likes + 1, tweet2.No_of_Likes);
            RemoveTweet(tweet2);
        }
        [Test]
        public void ReplyToTweetTest()
        {
            tweet tweet = new tweet { Message = "Test", User_Name = "test", Posted_On = DateTime.Now.ToString() };
            _tweetService.postTweet(tweet);
            var filter = Builders<tweet>.Filter.Eq("User_Name", tweet.User_Name);
            tweet tweet1 = _tweets.Find(filter).FirstOrDefault();
            tweet1.Message = "Test1";
            _tweetService.replyToTweet(tweet1);
            tweet tweet2 = _tweets.Find(filter).FirstOrDefault();
            Assert.AreEqual(tweet2.RepliesList.Count, 1);
            RemoveTweet(tweet2);
        }
        public void RemoveTweet(tweet tweet)
        {
            var filter = Builders<tweet>.Filter.Eq("Username", tweet.User_Name);
            _tweets.DeleteOne(filter);
        }
    }
}

