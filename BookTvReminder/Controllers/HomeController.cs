using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookTvReminder.Domain;

namespace BookTvReminder.Controllers
{
  [HandleError]
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      ViewData["Message"] = "BookTV Schedule";
      ViewData["Segments"] = new SegmentService().GetSegments();

      return View();
    }

    public ActionResult Schedule()
    {
        ViewData["Message"] = "BookTV Schedule";
        ViewData["Segments"] = new SegmentService().GetSegments();

        return View();
    }

    public ActionResult About()
    {
      return View();
    }

    public ActionResult Test()
    {
      return View();
    }
    
    public ActionResult SegmentDetail()
    {
      var titleHash = Convert.ToInt32(RouteData.Values["ID"]);
      var segments = new SegmentService().GetSegments();

      //HACK: To keep working on UI, try to find one with the same title+airing, else return the last one in the list
      var segment = (segments.FirstOrDefault(s => (s.Title + s.Day + s.Time).GetHashCode() == titleHash) ?? segments.Last());

      ViewData.Model = segment;
      
      return View();
    }
  }
}
