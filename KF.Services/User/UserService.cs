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
            try
            {
                var user = userRepository.TableNoTracking.FirstOrDefault(s => s.UserId == userId);
                return user.ToModel();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            
        }

        public IEnumerable<UserModel> GetUsers()
        {
            try
            {
                var users = userRepository.TableNoTracking.Select(x => x.ToModel()).ToList();
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public UserModel CreateUser(UserModel user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException("Exception user is null");

                KF.Core.DomainModels.User userEntity = user.ToEntity();
                userRepository.Insert(userEntity);

                UserModel createdUser = GetUserById(userEntity.UserId);
                return createdUser;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public bool RemoveUserById(Guid userId)
        {
            try
            {
                var userEntity = userRepository.TableNoTracking.FirstOrDefault(x => x.UserId == userId);

                if (userEntity == null) return false;

                userRepository.Delete(userEntity);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public UserModel UpdateUser(UserModel user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException("Exception user is null");

                var userEntity = userRepository.TableNoTracking.FirstOrDefault(s => s.UserId == user.UserId);
                if (userEntity == null) return null;

                userRepository.Update(user.ToEntity());
                return GetUserById(user.UserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public UserModel ValidateUser(string username, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    throw new ArgumentNullException("username or password is null or empty");

                var userEntity = userRepository.TableNoTracking.FirstOrDefault(s => s.Username == username && s.Password == password);

                return userEntity.ToModel();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        #endregion
    }
}
