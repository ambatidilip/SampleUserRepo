using AutoMapper;

using SampleUserRepo.Models;
using SampleUserRepo.Resources;
using System.Collections.Generic;


namespace SampleUserRepo.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<CountryPreferenceResource, CountryPreference>();
           
        }
    }
}
