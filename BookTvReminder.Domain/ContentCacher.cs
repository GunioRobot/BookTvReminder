using System;
using System.IO;

namespace BookTvReminder.Domain
{
    //TODO: AHT - Check for read access, locking?

    public class ContentCacher
    {
        public bool CacheAvailable(string key)
        {
            var fileName = GetCacheFileName(key);

            if (!File.Exists(fileName))
                return false;

            if (File.GetLastWriteTime(fileName).Date < DateTime.Today)
                return false;

            return true;
        }

        public string ReadCache(string key)
        {
            return File.ReadAllText(GetCacheFileName(key));
        }

        public void WriteCache(string key, string value)
        {
            File.WriteAllText(GetCacheFileName(key), value);
        }

        internal string GetCacheFileName(string key)
        {
            const string dir = @"C:\temp\";

            return dir + key + ".txt";
        }
    }
}
