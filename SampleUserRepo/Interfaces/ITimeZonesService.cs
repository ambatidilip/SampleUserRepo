using SampleUserRepo.Models;
using SampleUserRepo.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleUserRepo.Interfaces
{
   public interface ITimeZonesService
    {
        List<TimeZoneInfo> GetAllTimeZones();


        List<Country> GetListOfCountries();

        HashSet<string> GetTimeZonesByCountryId(string countryCode);

    }
}
