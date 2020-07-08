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

        HashSet<string> GetTimeZoneId();

        List<Country> GetListOfCountries();

        HashSet<string> GetTimeZoneId(string CountryId);
    }
}
