using System.Web;
using HtmlAgilityPack;

namespace BookTvReminder.Domain
{
  public static class HmltNodeHelper
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
      if (nodes == null)
        return "";

      return DecodeHtml(nodes[index]);
    }

    public static string DecodeHtml(this HtmlNode value)
    {
      if (value == null)
        return "";

      return DecodeHtml(value.InnerHtml);
    }

    public static string DecodeHtml(this string value)
    {
      return HttpUtility.HtmlDecode(value).Trim();
    }
  }
}