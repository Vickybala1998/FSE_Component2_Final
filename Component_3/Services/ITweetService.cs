using com.tweetapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetapp.Services
{
    public interface ITweetService
    {
        List<tweet> getAllTweets();
        List<tweet> getTweetByUserId(string _userName);
        bool postTweet(tweet _tweet);
        bool deleteTweet(Guid id);
        bool updateTweet(tweet _tweet);
        void likeTweet(Guid id,string like);
        void replyToTweet(tweet tweet);

    }
}
