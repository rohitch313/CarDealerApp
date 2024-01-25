using MyApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.IService;

namespace MyApp.Service
{
    internal class ProfileInformationService : IProfileInformationService
    {

        private readonly HttpClient _httpClient;

        public ProfileInformationService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        public async Task<ProfileInformationDTO> GetProfileInformationAsync()
        {
            try
            {
                string jwtToken = await SecureStorage.GetAsync("JWTToken");
                // Set up HttpClient (base URL, headers, etc.) for your API calls
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);
                }
                HttpResponseMessage response = await _httpClient.GetAsync("http://10.0.2.2:5137/api/ProfileInformationAPI");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Use a try-catch block to handle both array and single object cases
                    try
                    {
                        var customerDtos = JsonConvert.DeserializeObject<List<ProfileInformationDTO>>(responseBody);

                        // For simplicity, assuming you want to return the first item in the list or a default object
                        return customerDtos?.Count > 0 ? customerDtos[0] : new ProfileInformationDTO();
                    }
                    catch (JsonSerializationException)
                    {
                        // If deserialization as a list fails, attempt deserialization as a single object
                        var customerDto = JsonConvert.DeserializeObject<ProfileInformationDTO>(responseBody);
                        return customerDto ?? new ProfileInformationDTO(); // Return a default object if customerDto is null
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
