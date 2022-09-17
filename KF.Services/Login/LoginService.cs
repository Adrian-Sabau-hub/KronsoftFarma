using KF.Core.Data;

namespace KF.Services.Login
{
    public class LoginService : ILoginService
    {
        #region Fields
        private readonly IRepository<Core.DomainModels.User> userRepository;

        #endregion

        #region Methods

        public bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new ArgumentNullException("username or password is null or empty");

            var userEntity = userRepository.TableNoTracking.FirstOrDefault(s => s.Username == username && s.Password == password);

            return userEntity != null;

        }


        #endregion
    }
}
