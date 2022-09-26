using KF.CommonModel.Models;
using KF.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

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
                if(userDetails != null)
                {
                    byte[] data = Convert.FromBase64String(userDetails);
                    string decodedString = Encoding.UTF8.GetString(data);
                    var details = decodedString.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    var username = details[0];
                    var password = details[1];

                    var user = _userService.ValidateUser(username, password);
                    return user;
                }
                else
                {
                    return null;
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
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
            }
        }

        // Get by id

        [Route("/api/Users/{id}")]
        [HttpGet]
        public UserModel Get([FromRoute] Guid id)
        {
            try
            {
                if (id != Guid.Empty)
                {
                    var user = _userService.GetUserById(id);
                    return user;
                }
                else
                {
                    return null;
                }
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
                if(user != null)
                {
                    UserModel createdUser = _userService.CreateUser(user);
                    return createdUser;
                }
                else
                {
                    return null;
                }
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
                if (id != Guid.Empty)
                {
                    _userService.RemoveUserById(id);
                    return true;
                }
                else
                {
                    return false;
                }
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
                if (id != Guid.Empty && user != null)
                {
                    UserModel updatedUser = _userService.UpdateUser(user);
                    return updatedUser;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

    }
}
