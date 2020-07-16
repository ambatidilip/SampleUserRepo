using System;
using System.Collections.Generic;

namespace SampleUserRepo.Models
{
    public partial class Users
    {
        public Users()
        {
            UserPreference = new HashSet<UserPreference>();
        }

        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public bool Active { get; set; }
        public bool Locked { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public Guid SysCreator { get; set; }
        public DateTime SysCreated { get; set; }
        public Guid SysModifier { get; set; }
        public DateTime SysModified { get; set; }
        public string CountryCode { get; set; }

        public virtual ICollection<UserPreference> UserPreference { get; set; }
    }
}
