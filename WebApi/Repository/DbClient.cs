using com.tweetapp.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace com.tweetapp.Repository
{
    public class DbClient:IDbClient
    {
        public readonly IMongoCollection<user> _users;
        public readonly IMongoCollection<tweet> _tweets;

        public DbClient(IOptions<DbSettings> _DbSettings)
        {
            var connectionString = new MongoClient(_DbSettings.Value.connectionString);
            var databaseName = connectionString.GetDatabase(_DbSettings.Value.databaseName);
            _users = databaseName.GetCollection<user>(_DbSettings.Value.collectionName[0]);
            _tweets = databaseName.GetCollection<tweet>(_DbSettings.Value.collectionName[1]);
        }

        public IMongoCollection<user> getUsersCollection() =>_users; 
        public IMongoCollection<tweet> getTweetsCollection() => _tweets;

    }
};
