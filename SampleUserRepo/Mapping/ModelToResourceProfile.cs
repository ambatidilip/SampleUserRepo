using AutoMapper;
using SampleUserRepo.Models;
using SampleUserRepo.Resources;
using System.Collections.Generic;

namespace SampleUserRepo.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<CountryPreference, CountryPreferenceResource>();      

            CreateMap<QueryResult<CountryPreference>, QueryResultResource<CountryPreferenceResource>>();

            CreateMap<CountryPreference, UserPreferenceResource>();
            CreateMap<UserPreference, UserPreferenceResource>();

        }
    }
}
