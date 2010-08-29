﻿/**********************************************************************************************
 * Copyright 2009 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"). You may not use this file 
 * except in compliance with the License. A copy of the License is located at
 *
 *       http://aws.amazon.com/apache2.0/
 *
 * or in the "LICENSE.txt" file accompanying this file. This file is distributed on an "AS IS"
 * BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations under the License. 
 *
 * ********************************************************************************************
 *
 *  Amazon Product Advertising API
 *  Signed Requests Sample Code
 *
 *  API Version: 2009-03-31
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace BookTvReminder.Domain.AWS
{
  public class ItemLookupSample
  {
    private const string DESTINATION = "ecs.amazonaws.com";

    private const string NAMESPACE = "http://webservices.amazon.com/AWSECommerceService/2009-03-31";
    private const string ITEM_ID = "0545010225";
    private const string isbn = "9780618968411";

    public static void Test(AwsKeyHelper keyHelper)
    {
      
      var helper = new SignedRequestHelper(keyHelper.GetAwsAccessKeyId(), keyHelper.GetAwsSecretKey(), DESTINATION);
    
      String requestUrl;
      String title;

      //TODO: AHT - Add AssociateId
      // Add Request week for caching key?

      /*
       * Here is an ItemLookup example where the request is stored as a dictionary.
       */
      IDictionary<string, string> r1 = new Dictionary<string, String>();
      r1["Service"] = "AWSECommerceService";
      r1["Version"] = "2009-03-31";
      //r1["Operation"] = "ItemLookup";
      r1["Operation"] = "ItemLookup";
      //r1["ItemId"] = ITEM_ID;
      r1["ItemId"] = isbn;
      r1["IdType"] = "ISBN";
      r1["SearchIndex"] = "Books";
      //r1["ResponseGroup"] = "Small";
      r1["ResponseGroup"] = "Tags,Reviews,EditorialReview";
      r1["TagsPerPage"] = "20";

      /* Random params for testing */
      //r1["AnUrl"] = "http://www.amazon.com/books";
      //r1["AnEmailAddress"] = "foobar@nowhere.com";
      //r1["AUnicodeString"] = "αβγδεٵٶٷٸٹٺチャーハン叉焼";
      //r1["Latin1Chars"] = "ĀāĂăĄąĆćĈĉĊċČčĎďĐđĒēĔĕĖėĘęĚěĜĝĞğĠġĢģĤĥĦħĨĩĪīĬĭĮįİıĲĳ";

      requestUrl = helper.Sign(r1);
      var response = new HtmlLoader().LoadUrl(requestUrl);

      var xDoc = XDocument.Parse(response);
      var x = xDoc.Descendants().Where(node => node.Name.LocalName == "Item").Select(item => Item.Create(item));
      x.Count();     
    }

    public static void Test2(AwsKeyHelper keyHelper)
    {
      SignedRequestHelper helper = new SignedRequestHelper(keyHelper.GetAwsAccessKeyId(), keyHelper.GetAwsSecretKey(), DESTINATION);

      /*
       * The helper supports two forms of requests - dictionary form and query string form.
       */
      String requestUrl;
      String title;

      /*
       * Here is an ItemLookup example where the request is stored as a dictionary.
       */
      IDictionary<string, string> r1 = new Dictionary<string, String>();
      r1["Service"] = "AWSECommerceService";
      r1["Version"] = "2009-03-31";
      r1["Operation"] = "ItemLookup";
      //r1["ItemId"] = ITEM_ID;
      r1["ItemId"] = isbn;
      r1["IdType"] = "ISBN";
      r1["SearchIndex"] = "Books";
      //r1["ResponseGroup"] = "Small";
      r1["ResponseGroup"] = "Tags";

      /* Random params for testing */
      r1["AnUrl"] = "http://www.amazon.com/books";
      r1["AnEmailAddress"] = "foobar@nowhere.com";
      r1["AUnicodeString"] = "αβγδεٵٶٷٸٹٺチャーハン叉焼";
      r1["Latin1Chars"] = "ĀāĂăĄąĆćĈĉĊċČčĎďĐđĒēĔĕĖėĘęĚěĜĝĞğĠġĢģĤĥĦħĨĩĪīĬĭĮįİıĲĳ";

      requestUrl = helper.Sign(r1);
      title = FetchTitle(requestUrl);

      System.Console.WriteLine("Method 1: ItemLookup Dictionary form.");
      System.Console.WriteLine("Title is \"" + title + "\"");
      System.Console.WriteLine();

      /*
       * Here is a CartCreate example where the request is stored as a dictionary.
       */
      IDictionary<string, string> r2 = new Dictionary<string, String>();
      r2["Service"] = "AWSECommerceService";
      r2["Version"] = "2009-03-31";
      r2["Operation"] = "CartCreate";
      r2["Item.1.OfferListingId"] = "Ho46Hryi78b4j6Qa4HdSDD0Jhan4MILFeRSa9mK+6ZTpeCBiw0mqMjOG7ZsrzvjqUdVqvwVp237ZWaoLqzY11w==";
      r2["Item.1.Quantity"] = "1";

      requestUrl = helper.Sign(r2);
      title = FetchTitle(requestUrl);

      System.Console.WriteLine("Method 1: CartCreate Dictionary form.");
      System.Console.WriteLine("Cart Item Title is \"" + title + "\"");
      System.Console.WriteLine();

      /*
       * Here is an example where the request is stored as a query-string:
       */

      /*
       * string requestString = "Service=AWSECommerceService&Version=2009-03-31&Operation=ItemLookup&ResponseGroup=Small&ItemId=" + ITEM_ID;
       */
      System.Console.WriteLine("Method 2: Query String form.");

      String[] Keywords = new String[] {
                "surprise!",
                "café",
                "black~berry",
                "James (Jim) Collins",
                "münchen",
                "harry potter (paperback)",
                "black*berry",
                "finger lickin' good",
                "!\"#$%'()*+,-./:;<=>?@[\\]^_`{|}~",
                "αβγδε",
                "ٵٶٷٸٹٺ",
                "チャーハン",
                "叉焼",
            };

      foreach (String keyword in Keywords)
      {
        String requestString = "Service=AWSECommerceService"
            + "&Version=2009-03-31"
            + "&Operation=ItemSearch"
            + "&SearchIndex=Books"
            + "&ResponseGroup=Small"
            + "&Keywords=" + keyword
            ;
        requestUrl = helper.Sign(requestString);
        title = FetchTitle(requestUrl);

        System.Console.WriteLine("Keyword=\"" + keyword + "\"; Title=\"" + title + "\"");
        System.Console.WriteLine();
      }

      String cartCreateRequestString =
          "Service=AWSECommerceService"
          + "&Version=2009-03-31"
          + "&Operation=CartCreate"
          + "&Item.1.OfferListingId=Ho46Hryi78b4j6Qa4HdSDD0Jhan4MILFeRSa9mK%2B6ZTpeCBiw0mqMjOG7ZsrzvjqUdVqvwVp237ZWaoLqzY11w%3D%3D"
          + "&Item.1.Quantity=1"
          ;
      requestUrl = helper.Sign(cartCreateRequestString);
      title = FetchTitle(requestUrl);

      System.Console.WriteLine("Cart Item Title=\"" + title + "\"");
      System.Console.WriteLine();


      System.Console.WriteLine("Hit Enter to end");
      System.Console.ReadLine();
    }

    private static string FetchTitle(string url)
    {
      try
      {
        WebRequest request = HttpWebRequest.Create(url);
        WebResponse response = request.GetResponse();
        XmlDocument doc = new XmlDocument();
        doc.Load(response.GetResponseStream());

        XmlNodeList errorMessageNodes = doc.GetElementsByTagName("Message", NAMESPACE);
        if (errorMessageNodes != null && errorMessageNodes.Count > 0)
        {
          String message = errorMessageNodes.Item(0).InnerText;
          return "Error: " + message + " (but signature worked)";
        }

        XmlNode titleNode = doc.GetElementsByTagName("Title", NAMESPACE).Item(0);
        string title = titleNode.InnerText;
        return title;
      }
      catch (Exception e)
      {
        System.Console.WriteLine("Caught Exception: " + e.Message);
        System.Console.WriteLine("Stack Trace: " + e.StackTrace);
      }

      return null;
    }
  }
}
