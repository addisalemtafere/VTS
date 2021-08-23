using Application.Contracts.Services.GoogleGeocodingService;
using Infrastructure.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infrastructure.Services.GoogleGeocodingService
{
    public class GeocodingService : IGeocodingService
    {

        public GoogleSettings _googleSettings;

        public GeocodingService(IOptions<GoogleSettings> googleSettings)
        {
            _googleSettings = googleSettings.Value;
        }
        public async Task<string> GetAddressLocationAsync(double lat, double lang)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{_googleSettings.BaseUrl}?key={_googleSettings.ApiKey}&lat={lat}&lon={lang}&format=json");
            var contents = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Locality>(contents).display_name;
            return result;



        }


    }

    public class Locality
    {
        public string display_name { get; set; }
    }

}
