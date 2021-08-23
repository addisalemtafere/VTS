using Application.Contracts.Services.GoogleGeocodingService;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infrastructure.Services.GoogleGeocodingService
{
    public class GeocodingService : IGeocodingService
    {
        public async Task<string> GetAddressLocationAsync(double lat, double lang)
        {
            var httpClient = new HttpClient();
            var response = httpClient.GetAsync($"https://us1.locationiq.com/v1/reverse.php?key=pk.2265917474aca329f57a03d55d64451b&lat={lat}&lon={lang}&format=json").Result;
            var contents = await response.Content.ReadAsStringAsync();
            var result =  JsonConvert.DeserializeObject<Locality>(contents).display_name;
            return result;



        }


    }

    public class Locality
    {
        public string display_name { get; set; }
    }

}
