using KF.Common.Model.Automapper;
using KF.CommonModel.Models;
using KF.Core.Data;

namespace KF.Services.User
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly IRepository<Core.DomainModels.User> userRepository;

        #endregion

        #region ctor
        public UserService(IRepository<Core.DomainModels.User> userRepository)
        {
            this.userRepository = userRepository;
        }

        #endregion

        #region Methods

        public UserModel GetUserById(Guid userId)
        {
            var user = userRepository.Table.FirstOrDefault(s => s.UserId == userId);
            return user.ToModel();
        }

        public IEnumerable<UserModel> GetUsers()
        {
            var users = userRepository.Table.Select(x => x.ToModel()).ToList();
            return users;
        }

        public UserModel CreateUser(UserModel user)
        {
            if (user == null)
                throw new ArgumentNullException("Exception user is null");

            KF.Core.DomainModels.User userEntity = user.ToEntity();
            userRepository.Insert(userEntity);

            UserModel createdUser = GetUserById(userEntity.UserId);
            return createdUser;
        }

        public bool RemoveUserById(Guid userId)
        {
            var userEntity = userRepository.Table.FirstOrDefault(x => x.UserId == userId);
            
            if (userEntity == null)  return false;

            userRepository.Delete(userEntity);

            return true;
        }

        public UserModel UpdateUser(UserModel user)
        {
            if (user == null)
                throw new ArgumentNullException("Exception user is null");
            
            var userEntity = userRepository.TableNoTracking.FirstOrDefault(s => s.UserId == user.UserId);
            if (userEntity == null) return null;

            userRepository.Update(user.ToEntity());
            return GetUserById(user.UserId);
        }

        #endregion
    }
}
