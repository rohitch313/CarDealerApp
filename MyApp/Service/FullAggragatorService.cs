using MyApp.IService;
using MyApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Service
{
    internal class FullAggragatorService : IFullAggragatorService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "http://10.0.2.2:5137/api/";
        private readonly string _makeApiLogUrl = "PVA_MakeAPI";
        private readonly string _modelApiLogUrl = "PVA_ModelAPI";
        private readonly string _variantApiLogUrl = "PVA_VariantAPI";
        private readonly string _yearApiLogUrl = "PVA_YearOfRegAPI";
        private readonly string _postMobileNumberApiUrl = "PV_NewCarDealerAPI/Post";
        private readonly string _postNumberApiUrl = "PV_OpenMarketAPI/Post";
        private readonly string _postAggregatorApiUrl = "PV_AggregatorAPI/Post";
        private readonly string _carvehicleRecordApiUrl = "PurchaseVehicle/VehicleRecord";
       

        public FullAggragatorService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        private async Task<IEnumerable<T>> GetApiResponse<T>(string apiUrl)
        {
            try
            {
                string jwtToken = await SecureStorage.GetAsync("JWTToken");
                // Set up HttpClient (base URL, headers, etc.) for your API calls
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);
                }
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    try
                    {
                        var dtos = JsonConvert.DeserializeObject<List<T>>(responseBody);
                        return dtos ?? new List<T>();
                    }
                    catch (JsonSerializationException)
                    {
                        var dto = JsonConvert.DeserializeObject<T>(responseBody);
                        return dto != null ? new List<T> { dto } : new List<T>();
                    }
                }
                else
                {
                    // Include additional information about the error, such as the URL or response content
                    throw new HttpRequestException($"Error: {response.StatusCode}. Response: {await response.Content.ReadAsStringAsync()}");
                }
            }
            catch (HttpRequestException ex)
            {
                // Include additional information about the error, such as the URL
                throw new HttpRequestException($"HTTP request error: {ex.Message}. URL: {apiUrl}");
            }
            catch (Exception ex)
            {
                // Include additional information about the error, such as the URL
                throw new Exception($"Exception: {ex.Message}. URL: {apiUrl}");
            }
        }

        private async Task<bool> PostDetails(string apiUrl, object data, string logUrl)
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
                    JsonConvert.SerializeObject(data),
                    Encoding.UTF8,
                    "application/json"
                );

                using HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, requestBody);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    LogError(logUrl, errorContent);
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
                LogError(logUrl, ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                LogError(logUrl, ex.Message);
                return false;
            }
        }

        private void LogError(string apiUrl, string errorMessage)
        {
            // Implement logging logic here (consider using a logging framework)
            Console.WriteLine($"Error calling API at {apiUrl}: {errorMessage}");
        }

        public async Task<IEnumerable<Agg_DropDownMakeDTO>> GetMakeData()
        {
            return await GetApiResponse<Agg_DropDownMakeDTO>(CombineApiUrl(_makeApiLogUrl));
        }

        public async Task<IEnumerable<Agg_DropDownModelDTO>> GetModelData()
        {
            return await GetApiResponse<Agg_DropDownModelDTO>(CombineApiUrl(_modelApiLogUrl));
        }

        public async Task<IEnumerable<Agg_DropDownVariantDTO>> GetVariantData()
        {
            return await GetApiResponse<Agg_DropDownVariantDTO>(CombineApiUrl(_variantApiLogUrl));
        }

        public async Task<IEnumerable<Agg_DropDownYORegisDTO>> GetYearOfRegData()
        {
            return await GetApiResponse<Agg_DropDownYORegisDTO>(CombineApiUrl(_yearApiLogUrl));
        }

        public async Task<bool> PostAggragatorDetails(PV_AggregatorDTO aggregatorDTO)
        {
            return await PostDetails(CombineApiUrl(_postAggregatorApiUrl), aggregatorDTO, _postMobileNumberApiUrl);
        }

        public async Task<bool> PostNewCarDealerDetails(PV_NewCarDealerDTO newCarDetails)
        {
            return await PostDetails(CombineApiUrl(_postMobileNumberApiUrl), newCarDetails, _postMobileNumberApiUrl);
        }

        public async Task<bool> PostOpenMarketDetails(PV_OpenMarketDTO newCarDetails)
        {
            return await PostDetails(CombineApiUrl(_postNumberApiUrl), newCarDetails, _postNumberApiUrl);
        }

        private string CombineApiUrl(string endpoint)
        {
            return $"{_apiBaseUrl}{endpoint}";
        }

        public async Task<IEnumerable<VehicleRecordsDto>> GetCarVehicleRecord()
        {
            return await GetApiResponse<VehicleRecordsDto>(CombineApiUrl(_carvehicleRecordApiUrl));
        }
    }
}
