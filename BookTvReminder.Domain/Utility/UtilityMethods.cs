using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookTvReminder.Domain.Utility
{
    public static class UtilityMethods
    {

        public static T ParseEnum<T>(string value, bool ignoreCase = true)
        {
            return (T) Enum.Parse(typeof(T), value, ignoreCase);
        }

    }
}
