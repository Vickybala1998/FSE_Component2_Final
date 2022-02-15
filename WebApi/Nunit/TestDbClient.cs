using com.tweetapp.Models;
using com.tweetapp.Repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetAppNunit
{
    class TestDbClient:IDbClient
    {
        public readonly IMongoCollection<user> _users;
        public readonly IMongoCollection<tweet> _tweets;

        public TestDbClient()
        {
            var connectionString = new MongoClient("mongodb://localhost:27017");
            var databaseName = connectionString.GetDatabase("TesttweetApp");
            _users = databaseName.GetCollection<user>("Users");
            _tweets = databaseName.GetCollection<tweet>("Tweets");
        }

        public IMongoCollection<user> getUsersCollection() => _users;
        public IMongoCollection<tweet> getTweetsCollection() => _tweets;
    }
}
