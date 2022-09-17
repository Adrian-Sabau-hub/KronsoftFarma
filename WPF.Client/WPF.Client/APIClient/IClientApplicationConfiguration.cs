namespace KF.WPF.Client.APIClient
{
    public interface IClientApplicationConfiguration
    {
        string ServerAddress { get; }
        string AppUri { get; }
        string ClientId { get; }
    }
}
