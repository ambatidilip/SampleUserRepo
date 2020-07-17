using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleUserRepo.Resources
{
    public class UserPreferenceResource
    {
        public Guid UserPreferenceId { get; set; }
        public Guid UserId { get; set; }
        public string CultureCode { get; set; }
        public string DateFormat { get; set; }
        public string TimeFormat { get; set; }
        public string TimeZoneId { get; set; }
    }
}
