using KF.WPF.Client.Core.Models;
using KF.WPF.Client.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KF.WPF.Client.Core.APIClient.RestServices
{
    public class UserRestService : RestServiceBase
    {
        public UserRestService(IHttpClientFactory httpClientFactory, IClientApplicationConfiguration configuration, IUserDataService userDataService) : base(httpClientFactory, configuration, userDataService)
        {
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            using (HttpClient client = GetClient())
            {
                HttpRequestMessage request = await PrepareRequestMessageAsync(HttpMethod.Get, string.Format("{0}/api/Users", serverAddress));
                var response = await client.SendAsync(request);
                var payload = DeserializeObject<List<UserModel>>(await response.Content.ReadAsStringAsync());
                return payload;
            }
        }

        public async Task<UserModel> GetUserAsync(Guid userId)
        {
            using (HttpClient client = GetClient())
            {
                HttpRequestMessage request = await PrepareRequestMessageAsync(HttpMethod.Get, string.Format("{0}/api/Users/{1}", serverAddress, userId));
                var response = await client.SendAsync(request);
                var payload = DeserializeObject<UserModel>(await response.Content.ReadAsStringAsync());
                return payload;
            }
        }

        public async Task<UserModel> CreateUserAsync(UserModel user)
        {
            using (HttpClient client = GetClient())
            {
                var request = PrepareRequestMessageAsync(HttpMethod.Post, string.Format("{0}/api/Users", serverAddress)).Result;
                var jsonPayload = SerializeObject(user);
                request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await client.SendAsync(request);
                var payload = DeserializeObject<UserModel>(response.Content.ReadAsStringAsync().Result);
                return payload;
            }
        }

        public async Task<UserModel> UpdateUserAsync(Guid userId, UserModel user)
        {
            using (HttpClient client = GetClient())
            {
                var request = await PrepareRequestMessageAsync(HttpMethod.Put, string.Format("{0}/api/Users/{1}", serverAddress, userId));
                var jsonPayload = SerializeObject(user);
                request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await client.SendAsync(request);
                var payload = DeserializeObject<UserModel>(response.Content.ReadAsStringAsync().Result);
                return payload;
            }
        }


        public async Task DeleteUserAsync(Guid userId)
        {
            using (HttpClient client = GetClient())
            {

                var request = await PrepareRequestMessageAsync(HttpMethod.Delete, string.Format("{0}/api/Users/{1}", serverAddress, userId));
                var response = await client.SendAsync(request);
                await Task.CompletedTask;
            }
        }

        public async Task<UserModel> ValidateUser(String username, String password)
        {
            try
            {
                using (HttpClient httpClient = GetClient())
                {
                    var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(username + ":" + password));
                    //Convert.FromBase64CharArray("YTpi")
                    
                    var json = JsonConvert.SerializeObject(encoded);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync(string.Format("{0}/api/ValidateUser/", serverAddress), content);
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<UserModel>(result);
                }
            }
            catch (Exception ex) 
            {
                return null;
            }

        }
    }
}