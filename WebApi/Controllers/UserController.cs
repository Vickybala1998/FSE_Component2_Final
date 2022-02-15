using Microsoft.AspNetCore.Mvc;
using com.tweetapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.tweetapp.Services;
using Microsoft.Extensions.Logging;

namespace com.tweetapp.Controllers
{
    [Route("api/v1.0/tweets")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _service;
        private readonly ILogger<UserController> _log;
        public UserController(IUserService Service,ILogger<UserController> Log)
        {
            _service = Service;
            _log = Log;
        }
        [HttpGet("users/all")]
        public IActionResult getAllUsers()
        {
            try
            {
                _log.LogInformation("get All Users started");
                List<user> Allusers = _service.getAllUsers();
                _log.LogInformation("get All Users Completed");
                return Ok(Allusers);
            }
            catch (Exception e)
            {

                _log.LogError(e.Message.ToString());
                return StatusCode(500);
            }
        }

        [HttpGet("user/search/{username}")]
        public IActionResult getUsersById(string username)
        {
            try
            {
                if (!username.Contains('@'))
                    username = '@' + username;
                List<user> _user = _service.getUserById(username);
                return Ok(_user);
            }
            catch (Exception e)
            {
                _log.LogError(e.Message.ToString());
                return StatusCode(500);
            }
        }

        [HttpPost("register")]
        public IActionResult registerUser([FromBody] user _user)
        {
            bool _isRegisterd;
            try
            {
                _log.LogInformation("Registration started");
                _isRegisterd = _service.registerUser(_user);
                if(_isRegisterd==true)
                {
                    _log.LogInformation("Registration completed");
                    return StatusCode(200);
                }
                else
                {
                    return StatusCode(400);
                }

            }
            catch (Exception e)
            {
                _log.LogError(e.Message.ToString());
                return StatusCode(500);
            }
        }

        [HttpPost("login")]
        public IActionResult userLogin([FromBody]user _user)
        {
            try
            {
                if(_service.userLogin(_user))
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(401);
                }  
            }
            catch(Exception e)
            {
                _log.LogError(e.Message.ToString());
                return StatusCode(500);
            }
        }

        [HttpPut("{username}/forgot")]
        public IActionResult forgotPassword([FromBody]user _user,string username)
        {
            try
            {
                _user.Login_id = username;
                if(_service.forgotPassword(_user))
                {
                    return StatusCode(200);
                }
                else
                {
                    return StatusCode(401);
                }
            }
            catch(Exception e)
            {
                _log.LogError(e.Message.ToString());
                return StatusCode(500);
            }
        }
    }
}
