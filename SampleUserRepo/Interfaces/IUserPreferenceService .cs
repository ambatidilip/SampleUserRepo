using SampleUserRepo.Models.Services;
using SampleUserRepo.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleUserRepo.Interfaces
{
    public interface IUserPreferenceService
    {
        Task<UserPreferenceResponse> GetAsync(string userId);

        Task<UserPreferenceResponse> PutAsync(UserPreferenceResource preference);
    }
}
