using System.Net.Http;

namespace KF.WPF.Client.Core.APIClient
{
    public interface IHttpClientFactory
    {
        HttpClient GetHttpClient();
    }
}
