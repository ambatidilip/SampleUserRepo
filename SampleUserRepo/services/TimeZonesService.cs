using NodaTime;
using NodaTime.Extensions;
using NodaTime.TimeZones;
using SampleUserRepo.Interfaces;
using SampleUserRepo.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SampleUserRepo.services
{
    public class TimeZonesService : ITimeZonesService
    {
        public List<TimeZoneInfo> GetAllTimeZones()
        {
            var timeZones = TimeZoneInfo.GetSystemTimeZones();
            return timeZones.ToList();
        }



        public List<Country> GetListOfCountries()
        {

            List<Country> countryList = new List<Country>();

            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.SpecificCultures);

            var mainList = cultures.Where(C => C.LCID != 127 & !C.IsNeutralCulture).ToList();

            var v = cultures.Where(C => C.LCID != 127 & !C.IsNeutralCulture).Select(C => new Country() { EnglishName = new RegionInfo(C.LCID).EnglishName, Code = new RegionInfo(C.LCID).Name }).ToList();

            countryList = v.GroupBy(C => new { C.EnglishName, C.Code }).Select(C => new Country() { Code = C.Key.Code, EnglishName = C.Key.EnglishName }).OrderBy(C => C.EnglishName).ToList();
            return countryList;
        }

        public HashSet<string> GetTimeZonesByCountryId(string countryCode)
        {

            var zoneIds = TzdbDateTimeZoneSource.Default.ZoneLocations.Where(x => x.CountryCode == countryCode).Select(c => c.ZoneId);

            return zoneIds.ToHashSet();

        }

    }
}
