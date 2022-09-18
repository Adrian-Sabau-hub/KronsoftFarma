using KF.WPF.Client.Services.Interfaces;

namespace KF.WPF.Client.Services
{
    public class UserDataService : IUserDataService
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
