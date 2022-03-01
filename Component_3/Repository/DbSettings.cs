using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetapp.Repository
{
    public class DbSettings
    {
        public string connectionString { get; set; }
        public string databaseName { get; set; }
        public string[] collectionName { get; set; }
        public string azureSender { get; set; }
        public string azureReceiver { get; set; }
    }
}
