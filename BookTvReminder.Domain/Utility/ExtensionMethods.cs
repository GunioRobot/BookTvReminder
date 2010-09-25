using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookTvReminder.Domain.Utility
{
  public static class ExtensionMethods
  {

    /// <summary>
    /// Performs regular string.Substring but also auto-shrinks length to fit string
    /// </summary>
    public static string SubstringOrDefault(this string value, int startIndex, int length)
    {
      return SubstringOrDefault(value, startIndex, length, "");
    }

    /// <summary>
    /// Performs regular string.Substring but also auto-shrinks length to fit string
    /// </summary>    
    /// <example>
    /// "abc".SubstringOrDefault(0,100) => "abc"
    /// </example>
    public static string SubstringOrDefault(this string value, int startIndex, int length, string ellipsis)
    {
      //Shortcircuit empty strings
      if (string.IsNullOrEmpty(value))
        return value;

      //Check if the string is longer than requested substring
      int requestedLengthDifference = value.Length - (startIndex + length);
      
      //If the string is shorter than the requested length, trim length to fit and do not throw exception.
      if (requestedLengthDifference < 0)
      {
        length += requestedLengthDifference;        
      }

      //If the string is longer than te requested length, append the ellipsis.
      if (requestedLengthDifference > 0)
      {
        return value.Substring(startIndex, length) + ellipsis;
      }

      return value.Substring(startIndex, length);
    }

    public static T FirstOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate, T defaultValue)
    {
      var value = source.FirstOrDefault(predicate);

      if (Equals(value, default(T)))
      {
        value = defaultValue;
      }

      return value;
    }

    public static T FirstOrDefault<T>(this IEnumerable<T> source, T defaultValue)
    {
      var value = source.FirstOrDefault();

      if (Equals(value, default(T)))
      {
        value = defaultValue;
      }

      return value;
    }

  }
}