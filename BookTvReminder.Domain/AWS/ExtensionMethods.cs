using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace BookTvReminder.Domain.AWS
{
  public static class ExtensionMethods
  {
    public static IEnumerable<XElement> Named(this IEnumerable<XElement> source, string name)
    {
      return source.Where(d => d.Name == name || d.Name.LocalName == name);
    }

    public static XElement FirstNamed(this IEnumerable<XElement> source, string name)
    {
      return Named(source, name).First();
    }

    public static XElement ElementNamed(this XElement source, string name)
    {
      return source.Elements().FirstNamed(name);
    }

    public static XElement ElementNamed(this IEnumerable<XElement> source, string name)
    {
      return source.Elements().FirstNamed(name);
    }

    public static IEnumerable<XElement> ElementsNamed(this XElement source, string name)
    {
      return source.Elements().Named(name);
    }

    public static IEnumerable<XElement> ElementsNamed(this IEnumerable<XElement> source, string name)
    {
      return source.Elements().Named(name);
    }
  }
}
