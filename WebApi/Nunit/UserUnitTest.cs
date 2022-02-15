using com.tweetapp.Models;
using com.tweetapp.Repository;
using com.tweetapp.Services;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetAppNunit;

namespace TweetAppNunit
{
    [TestFixture]
    public class UserUnitTest
    {
        private IDbClient _client=new TestDbClient();
        private userService _userService;
        private IMongoCollection<user> _users;
        private ILogger<userService> _log; //= new Mock<ILogger<userService>>();

        public UserUnitTest() {
            _userService = new userService(_client, _log);
            _users = _client.getUsersCollection();
        }
        private user GetUser(user user) => _users.Find(u => u.Login_id == user.Login_id).FirstOrDefault();

        [Test]
        public void registerUserTest()
        {
            user _user = new user { id = new Guid(), First_Name = "admin", Last_Name = "admin", Email = "admin@admin.com", Login_id = "admin1", Password = "admin", Contact_No = "111111111" };
            bool? add = _userService.registerUser(_user);
            Assert.IsTrue(add);
            RemoveUser(_user);
        }
        [Test]
        public void GetUsersTest()
        {
            
            user _user = new user { id = new Guid(), First_Name = "admin", Last_Name = "admin", Email = "admin@admin.com", Login_id = "admin1", Password = "admin", Contact_No = "111111111" };
            _userService.registerUser(_user);
            var users = _userService.getAllUsers();
            Assert.IsTrue(users.Count > 0);
            RemoveUser(_user);
        }
        [Test]
        public void LoginTest()
        {
            user _user = new user { id = new Guid(), First_Name = "admin", Last_Name = "admin", Email = "admin@admin.com", Login_id = "admin1", Password = "admin", Contact_No = "111111111" };
            _userService.registerUser(_user);
            bool login = _userService.userLogin(_user);
            Assert.IsTrue(login);
            RemoveUser(_user);
        }
        [Test]
        public void ForgotPasswordTest()
        {
            user _user = new user { id = new Guid(), First_Name = "admin", Last_Name = "admin", Email = "admin@admin.com", Login_id = "admin1", Password = "admin", Contact_No = "111111111" };
            _userService.registerUser(_user);
            string password = "abc";
            _user.Password = password;
            _userService.forgotPassword(_user);
            var user = GetUser(_user);
            Assert.AreEqual(user.Password, password);
            RemoveUser(_user);
        }
        public void RemoveUser(user user)
        {
            var filter = Builders<user>.Filter.Eq(x => x.Login_id, user.Login_id);
            _users.DeleteOne(filter);
        }

    }
}
