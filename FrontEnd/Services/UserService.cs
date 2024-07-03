using FrontEnd.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public UserService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiUrl = configuration["ApiUrl"]; // Ensure this is configured in appsettings.json
        }


        public async Task<ApiResponse<T>> GetApiResponse<T>(HttpResponseMessage response)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            var apirespone = JsonConvert.DeserializeObject<ApiResponse<T>>(responseBody);
            return apirespone;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}/api/user/getallusers");
            response.EnsureSuccessStatusCode();

            var responseBody = await GetApiResponse<List<User>>(response);
            return responseBody.Data;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}/api/user/{id}");
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<User>(responseBody);
        }

        public async Task CreateUserAsync(User user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_apiUrl}/api/user", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateUserAsync(int id, User user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_apiUrl}/api/user/{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteUserAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiUrl}/api/user/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}

