using System;
using System.Collections.Generic;

namespace SampleUserRepo.Models
{
    public partial class UserPreferences
    {
        public Guid UserId { get; set; }
        public string CountryCode { get; set; }
        public string Language { get; set; }
        public string DateFormat { get; set; }
        public string TimeFormat { get; set; }
        public string TimeZone { get; set; }
        public string NumberFormat { get; set; }
    }
}
