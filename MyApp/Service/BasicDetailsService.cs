using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MyApp.Models;
using Newtonsoft.Json;
using MyApp.IService;

namespace MyApp.Service
{
    // BasicDetailsService.cs
    public class BasicDetailsService : IBasicDetailsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _postUserDetailsApiUrl = "http://10.0.2.2:5137/api/UserInfoAPI/Post"; // Replace with the actual API endpoint

        public BasicDetailsService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        public async Task<List<DropDownStateDTO>> GetState()
        {
            try
            {
                string jwtToken = await SecureStorage.GetAsync("JWTToken");
                // Set up HttpClient (base URL, headers, etc.) for your API calls
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);
                }
                HttpResponseMessage response = await _httpClient.GetAsync("http://10.0.2.2:5137/api/UserInfoAPI/State");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<DropDownStateDTO>>(responseBody);
                }
                else
                {
                    // Handle non-success status codes
                    throw new Exception($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                throw new Exception($"Exception: {ex.Message}");
            }
        }

        public async Task<bool> PostUserDetails(BasicDetailsDTO userDetails)
        {
            try
            {
                string jwtToken = await SecureStorage.GetAsync("JWTToken");
                // Set up HttpClient (base URL, headers, etc.) for your API calls
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);
                }
                var requestBody = new StringContent(
                    JsonConvert.SerializeObject(userDetails),
                    Encoding.UTF8,
                    "application/json"
                );

                using HttpResponseMessage response = await _httpClient.PostAsync(_postUserDetailsApiUrl, requestBody);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Bad Request: {errorContent}");

                    // Parse the JSON error content into a structured object
                    var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(errorContent);

                    // Example: Display specific error message to the user
                    Console.WriteLine($"Bad Request: Error: {problemDetails.Title}. {problemDetails.Detail}");

                    return false;
                }
                else
                {
                    response.EnsureSuccessStatusCode(); // Re-throw for other non-success status codes
                    return false; // This line might not be reached, depending on the response status code
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request error: {ex.Message}");
                // Assuming this is a mobile app, use the appropriate method for displaying alerts or logging
                // e.g., DisplayAlert("Request Error", $"HTTP request error: {ex.Message}", "OK");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                // Assuming this is a mobile app, use the appropriate method for displaying alerts or logging
                // e.g., DisplayAlert("Error", $"Exception: {ex.Message}", "OK");
                return false;
            }
        }

        // Create a class to represent the Problem Details structure
        public class ProblemDetails
        {
            public string Title { get; set; }
            public string Detail { get; set; }
            // Add other properties if the Problem Details response contains more details
        }
    }
}
