using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleUserRepo.Constants
{
    public static class TimeFormats
    {
        /// <summary>
        /// Returns 24Hr format HH:MM
        /// Example: 18:40
        /// </summary>
        public const string HH_COLON_MM = "HH:MM";

        /// <summary>
        /// Returns HH:MM
        /// Example: 05:30 PM
        /// </summary>
        public const string HH_COLON_MM_AM_PM = "hh:mm tt";
    }
}
