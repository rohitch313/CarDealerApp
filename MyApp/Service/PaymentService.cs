using MyApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.IService;
using MyApp.Models;

namespace MyApp.Services
{
    public class PaymentService : IPaymnetService
    {
        private readonly HttpClient _httpClient;

        public PaymentService()
        {
            _httpClient = new HttpClient();
            // Additional configurations for HttpClient can be done here
        }

        public async Task<List<PaymentDetailDto>> GetDuePayments()
        {
            // Your API logic here using HttpClient to fetch data
            try
            {
                string jwtToken = await SecureStorage.GetAsync("JWTToken");
                // Set up HttpClient (base URL, headers, etc.) for your API calls
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);
                }

                HttpResponseMessage response = await _httpClient.GetAsync("http://10.0.2.2:5137/api/Payment/due");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var paymentDtos = JsonConvert.DeserializeObject<List<PaymentDetailDto>>(responseBody);
                    return paymentDtos;


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
        public async Task<List<PaymentDetailDto>> GetPaymentStatus()
        {
            try
            {
                string jwtToken = await SecureStorage.GetAsync("JWTToken");
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);
                }
                HttpResponseMessage response = await _httpClient.GetAsync("http://10.0.2.2:5137/api/Payment/status");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var statusPay = JsonConvert.DeserializeObject<List<PaymentDetailDto>>(responseBody);
                    return statusPay;
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

        public async Task<List<PaymentDetailDto>> GetUpcomingPayment()
        {
            try
            {
                string jwtToken = await SecureStorage.GetAsync("JWTToken");
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);
                }
                HttpResponseMessage response = await _httpClient.GetAsync("http://10.0.2.2:5137/api/Payment/upcoming");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var statusPay = JsonConvert.DeserializeObject<List<PaymentDetailDto>>(responseBody);
                    return statusPay;
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
        public async Task<PaymentDetailDto> GetPaymentDetails(int paymentId)
        {
            try
            {
                string jwtToken = await SecureStorage.GetAsync("JWTToken");
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);
                }
                HttpResponseMessage response = await _httpClient.GetAsync($"http://10.0.2.2:5137/api/Payment/details/{paymentId}");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var paymentDetails = JsonConvert.DeserializeObject<PaymentDetailDto>(responseBody);
                    return paymentDetails;
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

        public async Task<string> PaymentProof(int Id, PaymentProofImgDTO paymentProof)
        {
            try
            {
                var requestBody = new StringContent(
                    JsonConvert.SerializeObject(paymentProof),
                    Encoding.UTF8,
                    "application/json"
                );


                using var response = await _httpClient.PostAsync($"http://10.0.2.2:5137/api/PaymentProofImg/PaymentPic?Id={Id}", requestBody);


                if (response.IsSuccessStatusCode)
                {

                    // Assuming your response body is a JWT token
                    var token = await response.Content.ReadAsStringAsync();

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

    }
}

    


