using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace BookTvReminder.Domain.AWS
{
  public class Item
  {

    public IEnumerable<Tag> Tags { get; set; }
    public IEnumerable<CustomerReview> CustomerReviews { get; set; }
    public IEnumerable<EditorialReview> EditorialReviews { get; set; }

    public string AverageRating { get; set; }
    public string TotalReviews { get; set; }

    public static Item Create(XElement itemNode)
    {
      var tagsNode = itemNode.ElementsNamed("Tags");
      var tags = tagsNode.ElementsNamed("Tag").Select(t =>
        new Tag
        {
          Name = t.ElementNamed("Name").Value,
          Usages = t.ElementNamed("TotalUsages").Value
        });

      var reviewsNode = itemNode.ElementsNamed("CustomerReviews");
      var avgRating = reviewsNode.ElementNamed("AverageRating").Value;
      var totalReviews = reviewsNode.ElementNamed("TotalReviews").Value;
      var reviews = reviewsNode.ElementsNamed("Review").Select(t =>
        new CustomerReview
        {
          Rating = t.ElementNamed("Rating").Value,
          Date = t.ElementNamed("Date").Value,
          Summary = t.ElementNamed("Content").Value,
          Content = t.ElementNamed("Content").Value
        });

      var editorialsNode = itemNode.ElementsNamed("EditorialReviews").First();
      var editorials = editorialsNode.ElementsNamed("EditorialReview").Select(t =>
        new EditorialReview
        {
          Source = t.ElementNamed("Source").Value,
          Content = t.ElementNamed("Content").Value
        });

      return new Item
      {
        Tags = tags,
        AverageRating = avgRating,
        TotalReviews = totalReviews,
        CustomerReviews = reviews,
        EditorialReviews = editorials,
      };

    }

  }

  public class Tag
  {
    public string Name { get; set; }
    public string Usages { get; set; }
  }

  public class EditorialReview
  {
    public string Source { get; set; }
    public string Content { get; set; }
  }

  public class CustomerReview
  {
    public string Rating { get; set; }
    public string Date { get; set; }
    public string Summary { get; set; }
    public string Content { get; set; }
  }
}
