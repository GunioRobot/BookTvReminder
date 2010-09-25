using System.Net;

namespace BookTvReminder.Domain
{
    public class HtmlLoader
    {
        private readonly ContentCacher contentCacher;

        public HtmlLoader()
        {
            contentCacher = new ContentCacher();
        }

        public string LoadUrl(string url)
        {
            //TODO: AHT - Is there any reason to create a Uri object? To validate Url or something?
            var cacheKey = GetUrlCacheKey(url);

            if (contentCacher.CacheAvailable(cacheKey))
            {
                return contentCacher.ReadCache(cacheKey);
            }

            return ReadContentsFromUrl(url, cacheKey);
        }

        protected virtual string ReadContentsFromUrl(string url, string cacheKey)
        {
            string html;
            using (var client = new WebClient())
            {
                html = client.DownloadString(url);
            }

            contentCacher.WriteCache(cacheKey, html);

            return html;
        }

        public string GetUrlCacheKey(string url)
        {
            return url.GetHashCode().ToString();
        }

    }
}
