using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookTvReminder.Domain.Utility
{
    public static class ExceptionHelper
    {
        public static void AssertNotNull<T>(T value, string paramName = "value") where T : class
        {
            if (value == null) throw new ArgumentNullException(paramName);
        }
    }
}
