namespace KF.WPF.Client.Core.APIClient
{
    public interface IClientApplicationConfiguration
    {
        string ServerAddress { get; }
        string AppUri { get; }
        string ClientId { get; }
    }
}
