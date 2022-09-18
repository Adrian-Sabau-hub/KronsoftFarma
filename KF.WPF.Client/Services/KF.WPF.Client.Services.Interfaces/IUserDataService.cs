namespace KF.WPF.Client.Services.Interfaces
{
    public interface IUserDataService
    {
        string Username { get; set; }
        string Password { get; set; }
        bool IsAdmin { get; set; }
    }
}
