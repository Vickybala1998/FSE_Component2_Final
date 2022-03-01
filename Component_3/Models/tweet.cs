using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;


namespace com.tweetapp.Models
{
    public class tweet
    {
        [BsonId]
        public Guid Id { get; set; }
        public string User_Name { get; set; }
        public string Message { get; set; }
        public string Posted_On { get; set; }
        public int No_of_Likes { get; set; }
        public int No_of_DisLikes { get; set; }
        public string Reply_Message { get; set; }
        public List<Dictionary<string,string>> RepliesList { get; set; }
    }
}
