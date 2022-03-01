using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;


namespace com.tweetapp.Models
{
    public class user
    {
        [BsonId]
        public Guid id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Login_id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Contact_No { get; set; }
        public string Created_On { get; set; }


    }
}
