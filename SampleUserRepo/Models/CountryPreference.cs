using System;
using System.Collections.Generic;

namespace SampleUserRepo.Models
{
    public partial class CountryPreference
    {
        public Guid CountryPreferenceId { get; set; }
        public string CountryCode { get; set; }
        public string CultureCode { get; set; }
        public string DateFormat { get; set; }
        public string TimeFormat { get; set; }
        public string TimeZoneId { get; set; }
        public Guid SysCreator { get; set; }
        public DateTime SysCreated { get; set; }
        public Guid SysModifier { get; set; }
        public DateTime SysModified { get; set; }
    }
}
