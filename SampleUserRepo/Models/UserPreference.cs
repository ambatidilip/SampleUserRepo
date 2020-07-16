using System;
using System.Collections.Generic;

namespace SampleUserRepo.Models
{
    public partial class UserPreference
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string CultureCode { get; set; }
        public string DateFormat { get; set; }
        public string TimeFormat { get; set; }
        public string TimeZoneId { get; set; }
        public Guid SysCreator { get; set; }
        public DateTime SysCreated { get; set; }
        public Guid SysModifier { get; set; }
        public DateTime SysModified { get; set; }

        public virtual Users User { get; set; }
    }
}
