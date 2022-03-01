using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using com.tweetapp.Models;

namespace com.tweetapp.Repository
{
    public interface IDbClient
    {
        IMongoCollection<user> getUsersCollection();
        IMongoCollection<tweet> getTweetsCollection();
    }
}
