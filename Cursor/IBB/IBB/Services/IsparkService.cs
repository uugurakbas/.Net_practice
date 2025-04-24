using System;
using System.Net.Http;
using System.Threading.Tasks;
using IBB.Models;
using Newtonsoft.Json;

namespace IBB.Services
{
    public interface IIsparkService
    {
        Task<List<IsparkModel>> GetIsparkDataAsync();
    }

    public class IsparkService : IIsparkService
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://data.ibb.gov.tr/api/3/action/datastore_search";

        public IsparkService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<IsparkModel>> GetIsparkDataAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"{ApiUrl}?resource_id=f4f56e58-5210-4f17-b852-effe356a890c");
                var isparkResponse = JsonConvert.DeserializeObject<IsparkResponse>(response);
                
                if (isparkResponse?.Success == true && isparkResponse.Result?.Records != null)
                {
                    return isparkResponse.Result.Records;
                }
                
                return new List<IsparkModel>();
            }
            catch (Exception ex)
            {
                // Log the error
                return new List<IsparkModel>();
            }
        }
    }
} 