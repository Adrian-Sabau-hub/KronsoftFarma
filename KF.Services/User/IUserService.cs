using KF.CommonModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KF.Services.User
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetUsers();
        UserModel GetUserById(Guid userId);
        bool RemoveUserById(Guid userId);
        UserModel CreateUser(UserModel user);
        UserModel UpdateUser(UserModel user);
        bool ValidateUser(string username, string password);
    }
}
