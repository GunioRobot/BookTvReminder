using System.Web;
using HtmlAgilityPack;

namespace BookTvReminder.Domain.Utility
{
    public static class HtmlNodeExtensionMethods
    {
        public static HtmlNode GetChildById(this HtmlNode node, string controlid)
        {
            return GetChildById(node, "", controlid);
        }

        public static HtmlNode GetChildById(this HtmlNode node, string controlType, string controlid)
        {
            string ctl = string.IsNullOrEmpty(controlType) ? "*" : controlType;
            string selector = ".//" + ctl + @"[contains(@id,'" + controlid + "')]";
            return node.SelectSingleNode(selector);
        }

        public static HtmlDocument LoadFromHtml(this HtmlDocument htmlDocument, string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            return doc;
        }

        public static string DecodeHtml(this HtmlNodeCollection nodes, int index)
        {
            return nodes == null ? "" : DecodeHtml(nodes[index]);
        }

        public static string DecodeHtml(this HtmlNode value)
        {
            return value == null ? "" : DecodeHtml(value.InnerHtml);
        }

        public static string DecodeHtml(this string value)
        {
            ExceptionHelper.AssertNotNull(value);

// ReSharper disable PossibleNullReferenceException
            return HttpUtility.HtmlDecode(value).Trim();
// ReSharper restore PossibleNullReferenceException
        }
    }
}