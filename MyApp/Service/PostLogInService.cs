// Service

using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Twilio.Types;
using MyApp.IService;
using MyApp.Models;
using MyApp.Model;

namespace MyApp.Service
{
    public class PostLogInService : IPostLogInService
    {
        private readonly HttpClient _httpClient;
        private readonly string _jwtToken;


        public PostLogInService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _jwtToken = SecureStorage.GetAsync("JWTToken").Result; // Retrieve the stored JWT token
                                                                   // Set up HttpClient (base URL, headers, etc.) for your API calls
            if (!string.IsNullOrEmpty(_jwtToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _jwtToken);
            }
        }


        public async Task<bool> PostLoginNumberAsync(string mobileNumber)
        {
            try
            {
                var requestBody = new StringContent(
                    JsonConvert.SerializeObject(new { PhoneNumber = mobileNumber }),
                    Encoding.UTF8,
                    "application/json"
                );

                using var response = await _httpClient.PostAsync($"http://10.0.2.2:5137/api/LoginUserPhoneAPI/generateotp/?phoneNumber={mobileNumber}", requestBody);

                if (response.IsSuccessStatusCode)
                {
                    // Assuming your response body is a JWT token
                    var token = await response.Content.ReadAsStringAsync();

                    // You can handle the token as needed (e.g., store it securely)
                    // For simplicity, I'm just printing it here
                    Console.WriteLine($"Token: {token}");

                    return true;
                }

                return false;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }
        public async Task<bool> ResendOtp(string mobileNumber)
        {
            try
            {
                var requestBody = new StringContent(
                    JsonConvert.SerializeObject(new { PhoneNumber = mobileNumber }),
                    Encoding.UTF8,
                    "application/json"
                );

                using var response = await _httpClient.PostAsync($"http://10.0.2.2:5137/api/LoginUserPhoneAPI/resend-otp/?phoneNumber={mobileNumber}", requestBody);

                if (response.IsSuccessStatusCode)
                {
                    // Assuming your response body is a JWT token
                    var token = await response.Content.ReadAsStringAsync();

                    // You can handle the token as needed (e.g., store it securely)
                    // For simplicity, I'm just printing it here
                    Console.WriteLine($"Token: {token}");

                    return true;
                }

                return false;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }



        public async Task<string> Verify(string mobileNumber, int? otp)
        {
            try
            {
                var requestBody = new StringContent(
                    JsonConvert.SerializeObject(new { PhoneNumber = mobileNumber, OTP = otp }),
                    Encoding.UTF8,
                    "application/json"
                );


                using var response = await _httpClient.PostAsync($"http://10.0.2.2:5137/api/LoginUserPhoneAPI/verifyotp?phoneNumber={mobileNumber}&enteredOTP={otp}", requestBody);


                if (response.IsSuccessStatusCode)
                {

                    // Assuming your response body is a JWT token
                    var token = await response.Content.ReadAsStringAsync();
                    SecureStorage.Default.SetAsync("JWTToken", token);
                    // You can handle the token as needed (e.g., store it securely)
                    // For simplicity, I'm just printing it here
                    Console.WriteLine($"Token: {token}");

                    return token;
                }

                return "Failed";
            }
            catch (HttpRequestException)
            {
                return "Failed";
            }
        }

        public async Task<bool> Logout()
        {
            try
            {

                string jwtToken = await SecureStorage.GetAsync("JWTToken");
                // Set up HttpClient (base URL, headers, etc.) for your API calls
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);
                }
                HttpResponseMessage response = await _httpClient.PostAsync("http://10.0.2.2:5137/api/LoginUserPhoneAPI/logout", null);


                if (response.IsSuccessStatusCode)
                {
                    // Clear the stored JWT token upon successful logout
                    SecureStorage.Default.Remove("JWTToken");
                    _httpClient.DefaultRequestHeaders.Authorization = null;
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                //Handle exceptions (logging, error messages, etc.)
                return false;
            }
        }
        public async Task<bool> Signup(string mobileNumber)
        {
            try
            {
                var requestBody = new StringContent(
                    JsonConvert.SerializeObject(new { PhoneNumber = mobileNumber }),
                    Encoding.UTF8,
                    "application/json"
                );

                using var response = await _httpClient.PostAsync($"http://10.0.2.2:5137/api/LoginUserPhoneAPI/Register/?phone={mobileNumber}", requestBody);

                if (response.IsSuccessStatusCode)
                {
                    // Assuming your response body is a JWT token
                    var token = await response.Content.ReadAsStringAsync();

                    // You can handle the token as needed (e.g., store it securely)
                    // For simplicity, I'm just printing it here
                    Console.WriteLine($"Token: {token}");

                    return true;
                }

                return false;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }


        public async Task<string> VerifySignup(string mobileNumber, int? otp)
        {
            try
            {
                var requestBody = new StringContent(
                    JsonConvert.SerializeObject(new { PhoneNumber = mobileNumber, OTP = otp }),
                    Encoding.UTF8,
                    "application/json"
                );


                using var response = await _httpClient.PostAsync($"http://10.0.2.2:5137/api/LoginUserPhoneAPI/verifyotpSignup?phoneNumber={mobileNumber}&enteredOTP={otp}", requestBody);


                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();

                    SecureStorage.Default.SetAsync("JWTToken", token);
                    // You can handle the token as needed (e.g., store it

                    // Assuming your response body is a JWT token


                    // You can handle the token as needed (e.g., store it securely)
                    // For simplicity, I'm just printing it here


                    return "Sucess";
                }

                return "Failed";
            }
            catch (HttpRequestException)
            {
                return "Failed";
            }
        }

        public async Task<string> SignUpComplete(string mobileNumber, BasicDetailsDTO basicDetailsDTO)
        {
            try
            {
                var requestBody = new StringContent(
                    JsonConvert.SerializeObject(basicDetailsDTO),
                    Encoding.UTF8,
                    "application/json"
                );


                using var response = await _httpClient.PostAsync($"http://10.0.2.2:5137/api/LoginUserPhoneAPI/AdditionalDetails?phone={mobileNumber}", requestBody);


                if (response.IsSuccessStatusCode)
                {

                    // Assuming your response body is a JWT token
                    var token = await response.Content.ReadAsStringAsync();

                    await SecureStorage.Default.SetAsync("JWTToken", token);

                    return "Sucess";
                }

                return "Failed";
            }
            catch (HttpRequestException)
            {
                return "Failed";
            }
        }

        public async Task<List<DropDownStateDTO>> GetState()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("http://10.0.2.2:5137/api/StateAPI");

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

        public async Task<UserStatusDto> GetUserStatus(string phoneNumber)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"http://10.0.2.2:5137/api/LoginUserPhoneAPI/userstatus?phoneNumber={phoneNumber}");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<UserStatusDto>(responseBody);
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





    }
}

