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

            /* var v = cultures.Where(C => C.LCID != 127 & !C.IsNeutralCulture).Select(C => new { new RegionInfo(C.LCID).EnglishName, new RegionInfo(C.LCID).Name, CultureName = C.Name, CultureCode = C.Name  }).ToList();*/
             var v = cultures.Where(C => C.LCID != 127 & !C.IsNeutralCulture).Select(C => new Country() { EnglishName = new RegionInfo(C.LCID).EnglishName, Code = new RegionInfo(C.LCID).Name}).ToList();

            countryList = v.GroupBy(C => new { C.EnglishName, C.Code }).Select(C => new Country() { Code = C.Key.Code, EnglishName = C.Key.EnglishName }).OrderBy(C => C.EnglishName).ToList();

            string countryId = "AR";

            var zoneIds = TzdbDateTimeZoneSource.Default.ZoneLocations.Where(x => x.CountryCode == countryId);
            // var zones = this.GetTimeZoneId();
            var _listIds = zoneIds.ToList();
            return countryList;
        }


        public HashSet<string> GetTimeZoneId()
        {
          return new HashSet<string>(TimeZoneInfo.GetSystemTimeZones().Select(tz => tz.Id));
        }

        public HashSet<string> GetTimeZoneId(string CountryId)
        {
            return new HashSet<string>(TimeZoneInfo.GetSystemTimeZones().Where(x => x.DisplayName == CountryId).Select(tz => tz.Id));
        }
    }
}
