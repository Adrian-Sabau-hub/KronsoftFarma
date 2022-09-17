using System.Net.Http;

namespace KF.WPF.Client.APIClient
{
    public interface IHttpClientFactory
    {
        HttpClient GetHttpClient();
    }
}
