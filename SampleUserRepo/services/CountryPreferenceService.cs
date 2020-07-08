using NodaTime.TimeZones;
using SampleUserRepo.Context;
using SampleUserRepo.Interfaces;
using SampleUserRepo.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleUserRepo.services
{
    public class CountryPreferenceService: ICountryPreferenceService
    {
        private readonly crsuserauthdeContext _context;
        public CountryPreferenceService(crsuserauthdeContext context)
        {
            _context = context;
        }

        public CountryPreference GetPreferenceByCountryId(string countryId)
        {
           var list =  this.GetTimeZonesByCountryId(countryId);
            return this._context.CountryPreference.FirstOrDefault((x) => x.TimeZone == list.FirstOrDefault());
        }

        public List<string> GetTimeZonesByCountryId(string countryId)
        {

            var zoneIds = TzdbDateTimeZoneSource.Default.ZoneLocations.Where(x => x.CountryCode == countryId).Select(c => c.ZoneId);

            return zoneIds.ToList();

        }
    }
}
