using KF.WPF.Client.Core.Models;
using KF.WPF.Client.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KF.WPF.Client.Core.APIClient.RestServices
{
    public class ProductRestService : RestServiceBase
    {
        public ProductRestService(IHttpClientFactory httpClientFactory, IClientApplicationConfiguration configuration, IUserDataService userDataService) : base(httpClientFactory, configuration, userDataService)
        {
        }

        public async Task<List<ProductModel>> GetAllProductsAsync()
        {
            using (HttpClient client = GetClient())
            {
                HttpRequestMessage request = await PrepareRequestMessageAsync(HttpMethod.Get, string.Format("{0}/api/Products", serverAddress));
                var response = await client.SendAsync(request);
                var payload = DeserializeObject<List<ProductModel>>(await response.Content.ReadAsStringAsync());
                return payload;
            }
        }

        public async Task<ProductModel> GetProductAsync(Guid productId)
        {
            using (HttpClient client = GetClient())
            {
                HttpRequestMessage request = await PrepareRequestMessageAsync(HttpMethod.Get, string.Format("{0}/api/Products/{1}", serverAddress, productId));
                var response = await client.SendAsync(request);
                var payload = DeserializeObject<ProductModel>(await response.Content.ReadAsStringAsync());
                return payload;
            }
        }

        public async Task<ProductModel> CreateProductAsync(ProductModel product)
        {
            using (HttpClient client = GetClient())
            {
                var request = PrepareRequestMessageAsync(HttpMethod.Post, string.Format("{0}/api/Products", serverAddress)).Result;
                var jsonPayload = SerializeObject(product);
                request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await client.SendAsync(request);
                var payload = DeserializeObject<ProductModel>(response.Content.ReadAsStringAsync().Result);
                return payload;
            }
        }

        public async Task<ProductModel> UpdateProductAsync(Guid productId, ProductModel product)
        {
            using (HttpClient client = GetClient())
            {
                var request = await PrepareRequestMessageAsync(HttpMethod.Put, string.Format("{0}/api/Products/{1}", serverAddress, productId));
                var jsonPayload = SerializeObject(product);
                request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await client.SendAsync(request);
                var payload = DeserializeObject<ProductModel>(response.Content.ReadAsStringAsync().Result);
                return payload;
            }
        }


        public async Task DeleteProductAsync(Guid productId)
        {
            using (HttpClient client = GetClient())
            {

                var request = await PrepareRequestMessageAsync(HttpMethod.Delete, string.Format("{0}/api/Products/{1}", serverAddress, productId));
                var response = await client.SendAsync(request);
                await Task.CompletedTask;
            }
        }
    }
}
