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
    public class UserPreferenceService : IUserPreferenceService
    {
        private readonly IDbRepository dbRepository;
        private readonly IMapper mapper;

        public UserPreferenceService( IDbRepository dbRepository, IMapper mapper)
        {
 
            this.dbRepository = dbRepository ?? throw new ArgumentNullException(nameof(dbRepository));
            this.mapper = mapper;
        }

        public async Task<UserPreferenceResponse> GetAsync(string userId)
        {
            return await Task.Run(() =>
            {
               try
                {
                    Guid Id = Guid.Parse(userId);


                    var userRecord = this.dbRepository.GetItems<Users>().Where(x => x.UserId == Id).FirstOrDefault();
                    if (userRecord == null)
                        return new UserPreferenceResponse(new ErrorMessage("NoRecordFound", $"No record found."));

                    var result = this.dbRepository.GetItems<UserPreference>().Where(x => x.UserId == Id).FirstOrDefault();
                    if (result == null)
                    {
                        var defaultRepo = this.dbRepository.GetItems<CountryPreference>().Where(x => x.CountryCode == userRecord.CountryCode).FirstOrDefault();
                       if(defaultRepo == null)
                        {
                            return new UserPreferenceResponse(new ErrorMessage("NoRecordFoundWithCountryCode", $"No record found with country code."));
                        }
                        else
                        {
                            var newPreference = mapper.Map<CountryPreference, UserPreferenceResource>(defaultRepo);
                            newPreference.UserId = Id;
                            newPreference.UserPreferenceId = Guid.NewGuid();
                            var userPreference = mapper.Map<UserPreferenceResource, UserPreference>(newPreference);
                            userPreference.SysCreated = DateTime.UtcNow;
                            userPreference.SysCreator = Id;
                            userPreference.SysModified = DateTime.UtcNow;
                            userPreference.SysModifier = Id;
                            dbRepository.InsertItem<UserPreference>(userPreference);
                            return new UserPreferenceResponse(newPreference);
                        }
                    }
                    else
                    {
                        var preference = mapper.Map<UserPreference, UserPreferenceResource>(result);
                        return new UserPreferenceResponse(preference);
                    }
                }catch(Exception ex)
                {
                    return new UserPreferenceResponse(new ErrorMessage("999", $"An error occurred when get user by id: {ex.Message}"));
                }
               
            });
        }

        public async Task<UserPreferenceResponse> PutAsync(UserPreferenceResource preference)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var userRecord = this.dbRepository.GetItems<Users>().Where(x => x.UserId == preference.UserId).FirstOrDefault();
                    if (userRecord == null)
                        return new UserPreferenceResponse(new ErrorMessage("NoRecordFound", $"No record found."));

                    var newPreference = mapper.Map<UserPreferenceResource, UserPreference>(preference);

                    dbRepository.UpdateItem<UserPreference>(newPreference);
                    return new UserPreferenceResponse(preference);
                }
                catch (Exception ex)
                {
                    return new UserPreferenceResponse(new ErrorMessage("999", $"An error occurred when get user by id: {ex.Message}"));
                }

            });
        }
    }
}
