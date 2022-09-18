using System.Net.Http;

namespace KF.WPF.Client.Core.APIClient
{
    public class HttpClientFactory : IHttpClientFactory
    {
        public HttpClient GetHttpClient()
        {
            return new HttpClient();
        }
    }
}
