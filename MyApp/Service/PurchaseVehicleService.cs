using MyApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using MyApp.IService;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Services
{
    public class PurchaseVehicleService:IPurchaseVehicleService
    {
        private readonly HttpClient _httpClient;

        public PurchaseVehicleService()
        {
            _httpClient = new HttpClient();
            // Additional configurations for HttpClient can be done here
        }
      public async Task<List<PurchaseVehicleRecordModel>> GetVehicleRecord()
        {
            try
            {
                string jwtToken = await SecureStorage.GetAsync("JWTToken");
                // Set up HttpClient (base URL, headers, etc.) for your API calls
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);
                }
                HttpResponseMessage response = await _httpClient.GetAsync("http://10.0.2.2:5137/api/PurchaseVehicle/VehicleRecord");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var purchaseRecord = JsonConvert.DeserializeObject<List<PurchaseVehicleRecordModel>>(responseBody);
                    return purchaseRecord;

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

      public async  Task<List<PurchaseVehicleDocModel>> GetVehicleRecordById(int Id)
    {
        try
        {
                string jwtToken = await SecureStorage.GetAsync("JWTToken");
                // Set up HttpClient (base URL, headers, etc.) for your API calls
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);
                }
                HttpResponseMessage response = await _httpClient.GetAsync($"http://10.0.2.2:5137/api/Payment/details/{Id}");

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var paymentDetails = JsonConvert.DeserializeObject<List<PurchaseVehicleDocModel>>(responseBody);
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

}
}

