using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tweetServiceBus_Consumer
{
   public  class tweet
    {
        public string Id { get; set; }
        public string User_Name { get; set; }
        public string Message { get; set; }
        public string Posted_On { get; set; }
        public int No_of_Likes { get; set; }
        public int No_of_DisLikes { get; set; }
        public string Reply_Message { get; set; }
        public List<Dictionary<string, string>> RepliesList { get; set; }
    }
}
