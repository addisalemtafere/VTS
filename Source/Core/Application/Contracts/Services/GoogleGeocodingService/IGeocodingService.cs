using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Services.GoogleGeocodingService
{
    public interface IGeocodingService
    {
        public Task<string> GetAddressLocationAsync(double lat, double lang);
    }
}
