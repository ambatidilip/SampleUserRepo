using SampleUserRepo.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleUserRepo.Models.Services
{
    public class CountryPreferenceResponse : BaseResponse<CountryPreferenceResource>
    {
        public CountryPreferenceResponse(CountryPreferenceResource preference) : base(preference) { }
        public CountryPreferenceResponse(List<CountryPreferenceResource> preferences, int count = 0) : base(preferences, count) { }
        public CountryPreferenceResponse(ErrorMessage errorMessage) : base(errorMessage) { }

        public CountryPreferenceResponse(List<ErrorMessage> errorMessages) : base(errorMessages) { }
    }
}
