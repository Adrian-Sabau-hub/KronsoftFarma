using KF.CommonModel.Models;
using KF.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text;
using System.Text.Json;

namespace KF.Web.API.Controllers
{
    [Authorize]
    [ApiController]

    public class UserController : Controller
    {
        IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Login
        [AllowAnonymous]
        [Route("/api/ValidateUser")]
        [HttpPost]
        public UserModel ValidateUser([FromBody] string userDetails)
        {
            try
            {
                byte[] data = Convert.FromBase64String(userDetails);
                string decodedString = Encoding.UTF8.GetString(data);
                var details = decodedString.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                var username = details[0];
                var password = details[1];

                var user = _userService.ValidateUser(username, password);

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
                //return (IEnumerable<UserModel>)View();
            }
        }

        // Get

        [Route("/api/Users")]
        [HttpGet]
        public IEnumerable<UserModel> Get()
        {
            try
            {
                var user = _userService.GetUsers();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
                //return (IEnumerable<UserModel>)View();
            }
        }

        // Get by id

        [Route("/api/Users/{id}")]
        [HttpGet]
        public UserModel Get([FromRoute] Guid id)
        {
            try
            {
                var user = _userService.GetUserById(id);

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        // Create

        [Route("/api/Users")]
        [HttpPost]
        public UserModel Create([FromBody] UserModel user)
        {
            try
            {
                //var studentModel = JsonSerializer.Deserialize<StudentModel>(student);
                UserModel createdUser = _userService.CreateUser(user);
                return createdUser;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        // Delete

        [Route("/api/Users/{id}")]
        [HttpDelete]
        public bool RemoveUserById(Guid id)
        {
            try
            {
                _userService.RemoveUserById(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        // Update

        [Route("/api/Users/{id}")]
        [HttpPut]
        public UserModel Update(Guid id, [FromBody] UserModel user)
        {
            try
            {
                UserModel updatedUser = _userService.UpdateUser(user);
                return updatedUser;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

    }
}
