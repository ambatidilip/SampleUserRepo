using NodaTime.TimeZones;
using SampleUserRepo.Constants;
using SampleUserRepo.Interfaces;
using SampleUserRepo.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SampleUserRepo.services
{
    public class CountryPreferenceService: ICountryPreferenceService
    {

        private readonly ITimeZonesService _timezoneService;
        public CountryPreferenceService( ITimeZonesService timezoneService)
        {
            _timezoneService = timezoneService;
        }

        public CountryPreference GetPreferenceByCountryCode(string countryCode)
        {
            CountryPreference obj = new CountryPreference();
            var listOfCountries = this._timezoneService.GetListOfCountries();

            var country = listOfCountries.Where(x => x.Code == countryCode).FirstOrDefault();
            if(country == null)
            {
                throw new Exception("CountryIDNotFound");
            }
            else
            {
               
                var regionInfo = new RegionInfo(country.Code);
                string zoneId = this._timezoneService.GetTimeZonesByCountryId(country.Code).FirstOrDefault();

                CultureInfo cul = new CultureInfo(country.Code);
                var format = cul.NumberFormat;
                obj = new CountryPreference()
                {
                    CountryCode = country.Code,
                    DateFormat = cul.DateTimeFormat.ShortDatePattern,
                    Id = Guid.NewGuid(),
                    Language = LanguageFormats.ENGLISH_US,
                    // NumberFormat = regionInfo.CurrencySymbol, // Need to replace with reg-ex
                    TimeFormat = TimeFormats.HH_COLON_MM_AM_PM,
                    TimeZone = zoneId
                };
            }

            return obj;
        }
    }
}
