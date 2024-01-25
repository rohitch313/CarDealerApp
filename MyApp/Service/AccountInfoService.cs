using MyApp.IService;
using MyApp.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyApp.Service
{
    internal class AccountInfoService : IAccountInfoService
    {
        private readonly HttpClient _httpClient;

        public AccountInfoService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<AccountInfoDTO> GetAccountInfoDetails()
        {
            try
            {
                string jwtToken = await SecureStorage.GetAsync("JWTToken");
                // Set up HttpClient (base URL, headers, etc.) for your API calls
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);
                }
                HttpResponseMessage response = await _httpClient.GetAsync("http://10.0.2.2:5137/api/UserAccountAPI");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Use a try-catch block to handle both array and single object cases
                    try
                    {
                        var customerDtos = JsonConvert.DeserializeObject<List<AccountInfoDTO>>(responseBody);

                        // For simplicity, assuming you want to return the first item in the list or a default object
                        return customerDtos?.Count > 0 ? customerDtos[0] : new AccountInfoDTO();
                    }
                    catch (JsonSerializationException)
                    {
                        // If deserialization as a list fails, attempt deserialization as a single object
                        var customerDto = JsonConvert.DeserializeObject<AccountInfoDTO>(responseBody);
                        return customerDto ?? new AccountInfoDTO(); // Return a default object if customerDto is null
                    }
                }
                else
                {
                    throw new HttpRequestException($"Error: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"HTTP request error: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}");
            }
        }

    }
}
