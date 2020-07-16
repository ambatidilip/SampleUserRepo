using AutoMapper;
using SampleUserRepo.Interfaces;
using SampleUserRepo.Models;
using SampleUserRepo.Models.Services;
using SampleUserRepo.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleUserRepo.services
{
    public class CountryPreferenceService : ICountryPreferenceService
    {
        private readonly IDbRepository dbRepository;
        private readonly IMapper mapper;

        public CountryPreferenceService( IDbRepository dbRepository, IMapper mapper)
        {
 
            this.dbRepository = dbRepository ?? throw new ArgumentNullException(nameof(dbRepository));
            this.mapper = mapper;
        }

        public async Task<CountryPreferenceResponse> GetCountryPreference(string countryCode)
        {
            return await Task.Run(() =>
            {
                var result = this.dbRepository.GetItems<CountryPreference>().Where(x => x.CountryCode == "*").FirstOrDefault();
                var preference = mapper.Map<CountryPreference, CountryPreferenceResource>(result);
                return new CountryPreferenceResponse(preference);
            });

        }
    }
}
