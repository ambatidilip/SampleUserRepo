using SampleUserRepo.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleUserRepo.Models.Services
{
    public class UserPreferenceResponse : BaseResponse<UserPreferenceResource>
    {
        public UserPreferenceResponse(UserPreferenceResource preference) : base(preference) { }
        public UserPreferenceResponse(List<UserPreferenceResource> preferences, int count = 0) : base(preferences, count) { }
        public UserPreferenceResponse(ErrorMessage errorMessage) : base(errorMessage) { }

        public UserPreferenceResponse(List<ErrorMessage> errorMessages) : base(errorMessages) { }
    }
}
