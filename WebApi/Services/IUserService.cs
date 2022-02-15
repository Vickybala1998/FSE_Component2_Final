using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.tweetapp.Models;

namespace com.tweetapp.Services
{
    public interface IUserService
    {
        bool registerUser(user user);
        bool userLogin(user _user);
        bool forgotPassword(user _user);
        List<user> getAllUsers();
        List<user> getUserById(string _username);

    }
}
