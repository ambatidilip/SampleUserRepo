using SampleUserRepo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleUserRepo.Interfaces
{
   public interface ICountryPreferenceService
    {
       CountryPreference GetPreferenceByCountryId(string countryId);

        List<string> GetTimeZonesByCountryId(string countryId);
    }
}
