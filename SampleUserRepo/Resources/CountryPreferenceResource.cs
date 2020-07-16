using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleUserRepo.Resources
{
    public class CountryPreferenceResource
    {
        public Guid Id { get; set; }
        public string CountryCode { get; set; }
        public string CultureCode { get; set; }
        public string DateFormat { get; set; }
        public string TimeFormat { get; set; }
        public string TimeZoneId { get; set; }
    }
}
