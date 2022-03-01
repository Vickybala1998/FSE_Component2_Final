using com.tweetapp.Models;
using com.tweetapp.Repository;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetapp.Services
{
    public class tweetService : ITweetService
    {
        private readonly IMongoCollection<tweet> _tweets;
        private readonly ILogger<tweetService> _log;
        public tweetService(IDbClient dbClient,ILogger<tweetService> Log)
        {
            _tweets = dbClient.getTweetsCollection();
            _log = Log;
        }

        public List<tweet> getAllTweets()
        {
           List<tweet> getAllTweets= _tweets.Find(tweets => true).ToList();
            return Enumerable.Reverse(getAllTweets).ToList();
        }

        public List<tweet> getTweetByUserId(string _userName)
        {
            List<tweet> Tweets = _tweets.Find(tweets => tweets.User_Name == _userName).ToList();
            return Enumerable.Reverse(Tweets).ToList();
        }
        public void addTweet()
        {

        }

        public bool postTweet(tweet _tweet)
        {
            try
            {
                _tweet.Posted_On = DateTime.Now.ToString("dd.MMM.yyyy hh:mm tt");
                _tweet.No_of_Likes = 0;
                _tweet.RepliesList = new List<Dictionary<string, string>>() { };
                if (!string.IsNullOrEmpty(_tweet.Message) && !string.IsNullOrEmpty(_tweet.User_Name))
                {
                    _tweets.InsertOne(_tweet);
                    return true;
                }
                return false;
            }
            catch(Exception e)
            {
                _log.LogError(e.Message.ToString());
                return false;
            }
        }
        public bool updateTweet(tweet _tweet)
        {
            try
            {
                _tweet.Posted_On = DateTime.Now.ToString("dd.MMM.yyyy hh:mm tt");
                if (!string.IsNullOrEmpty(_tweet.Message))
                {
                    var filter = Builders<tweet>.Filter.Eq("Id", _tweet.Id);
                    var update = Builders<tweet>.Update.Set("Message", _tweet.Message).Set("Posted_On", _tweet.Posted_On);
                    _tweets.UpdateOne(filter, update);
                    return true;
                }
                return false;
            }
            catch(Exception e)
            {
                _log.LogError(e.Message.ToString());
                return false;
            }
        }

        public bool deleteTweet(Guid id)
        {
            try
            {
                var filter = Builders<tweet>.Filter.Eq("Id", id);
                _tweets.DeleteOne(filter);
                return true;
            }
            catch(Exception e)
            {
                _log.LogError(e.Message.ToString());
                return false;
            }
        }

        public void likeTweet(Guid id,string like)
        {
            try
            {
                var filter = Builders<tweet>.Filter.Eq("Id",id);
                tweet tweets = _tweets.Find(filter).FirstOrDefault();
                int likes = 1;
                if (like == "-1")
                {
                    likes = tweets.No_of_DisLikes + 1;
                    var updateDisLike = Builders<tweet>.Update.Set("No_of_DisLikes", likes);
                    _tweets.UpdateOne(filter, updateDisLike);
                }
                else
                {
                    likes = tweets.No_of_Likes + 1;
                    var updateLike = Builders<tweet>.Update.Set("No_of_Likes", likes);
                    _tweets.UpdateOne(filter, updateLike);
                }
                
                
            }
            catch(Exception e)
            {
                _log.LogError(e.Message.ToString());
            }

        }

        public void replyToTweet(tweet _tweet)
        {
            try
            {
                var filter = Builders<tweet>.Filter.Eq("Id", _tweet.Id);
                tweet tweets = _tweets.Find(filter).FirstOrDefault();
                tweets.RepliesList.Add(new Dictionary<string, string>()
                {
                    { "User_Name", _tweet.User_Name },
                    { "Reply_Message", _tweet.Reply_Message },
                    { "Posted_On", DateTime.Now.ToString("dd.MMM.yyyy hh:mm tt")}
                 });
                var update = Builders<tweet>.Update.Set("RepliesList", tweets.RepliesList);
                _tweets.UpdateOne(filter, update);
            }
            catch(Exception e)
            {
                _log.LogError(e.Message.ToString());

            }
        }
    }
}
