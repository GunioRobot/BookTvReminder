using System;
using System.IO;
using System.Net;

namespace BookTvReminder.Domain
{
  public class HtmlLoader
  {

    public string LoadUrl(string url)
    {
      //TODO: AHT - Is there any reason to create a Uri object?
      string html;
      var fileName = GetUrlFileName(url);

      //TODO: AHT - Check for read access, locking?

      if (UseCachedFile(fileName))
      {        
        html = File.ReadAllText(fileName);
      }
      else
      {
        using (var client = new WebClient())
        {
          html = client.DownloadString(url);
        }
        
        File.WriteAllText(fileName, html);
      }

      return html;
    }

    public string GetUrlFileName(string url)
    {
      //TODO: AHT - Make disk-cache directory configurable or in DB
      const string dir = @"C:\temp\";

      return dir + url.GetHashCode() + ".txt";
    }

    public bool UseCachedFile(string fileName)
    {
      if (!File.Exists(fileName))
        return false;      

      if (File.GetLastWriteTime(fileName).Date < DateTime.Today)
        return false;

      return true;
    }
  }
}
