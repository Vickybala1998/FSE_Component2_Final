using com.tweetapp.Models;
using com.tweetapp.Repository;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace com.tweetapp.Services
{
    public class userService : IUserService
    {
        private readonly IMongoCollection<user> _users;
        private readonly ILogger<userService> _log;
        public userService(IDbClient dbClient,ILogger<userService> Log)
        {
            _users = dbClient.getUsersCollection();
            _log = Log;
        }

        private user getUser(user _user)
        {
            return _users.Find(user => user.Login_id == _user.Login_id).FirstOrDefault();
        }

        public List<user> getAllUsers()
        {
            return _users.Find(user => user.Login_id!="admin").ToList();
        }
        public List<user> getUserById(string _username)
        {
            return _users.Find(user => user.Login_id == _username).ToList();
        }

        public bool registerUser(user _user)
        {
            try
            {
                user _userDetails = getUser(_user);
                _user.Created_On = DateTime.Now.ToString("dd/MMM/yyyy hh:mm tt");

                if (_userDetails == null && (!string.IsNullOrEmpty(_user.First_Name) && !string.IsNullOrEmpty(_user.Last_Name) && !string.IsNullOrEmpty(_user.Password)
                    && !string.IsNullOrEmpty(_user.Contact_No) && !string.IsNullOrEmpty(_user.Email) && !string.IsNullOrEmpty(_user.Login_id)))
                {
                    
                    _users.InsertOne(_user);
                    return true;

                }
                else if (_userDetails != null)
                {
                    return false;
                }
                return false;
            }
            catch (Exception e)
            {
                _log.LogError(e.Message.ToString());
                return false;
            }
        }

        public bool userLogin(user _user)
        {
            try
            { 
                user _userDetails = getUser(_user);
                if (_userDetails != null && _userDetails.Password == _user.Password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                _log.LogError(e.Message.ToString());
                return false;
            }
        }

        public bool forgotPassword(user _user)
        {
            try
            {
                user _userDetails = getUser(_user);
                if (_userDetails != null && _userDetails.Login_id==_user.Login_id)
                {
                    var filter = Builders<user>.Filter.Eq("Login_id", _user.Login_id);
                    var update = Builders<user>.Update.Set("Password", _user.Password);
                    _users.UpdateOne(filter, update);
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception e)
            {
                _log.LogError(e.Message.ToString());
                return false;
            }
        }
    }
}
