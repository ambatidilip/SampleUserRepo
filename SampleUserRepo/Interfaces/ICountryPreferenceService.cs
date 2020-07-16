using SampleUserRepo.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleUserRepo.Interfaces
{
    public interface ICountryPreferenceService
    {
        Task<CountryPreferenceResponse> GetCountryPreference(string countryCode);
    }
}
