using KF.CommonModel.Models;

namespace KF.Services.User
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetUsers();
        UserModel GetUserById(Guid userId);
        bool RemoveUserById(Guid userId);
        UserModel CreateUser(UserModel user);
        UserModel UpdateUser(UserModel user);
        UserModel ValidateUser(string username, string password);
    }
}
