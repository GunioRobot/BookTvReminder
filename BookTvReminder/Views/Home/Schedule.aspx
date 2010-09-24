<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="BookTvReminder.Domain" %>
<%@ Import Namespace="BookTvReminder.Domain.Utility" %>
<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">Home
  Page </asp:Content>
<asp:Content ID="indexHead" ContentPlaceHolderID="HeadContent" runat="server">
  <script type="text/javascript">

    $(document).ready(function () {

      $('a.segment').each(function () {

        var $this = $(this);
        var $dialog = $this.parent().siblings('div.ajax-dialog');

        //define config object
        var dialogOpts = {
          title: $this[0].title,
          modal: true,
          autoOpen: false,
          height: 500,
          width: 500,
          open: function () {
            //display correct dialog content
            $dialog.load($this[0].href);
          }
        };

        $dialog.dialog(dialogOpts);

        $this.click(function () {
          $dialog.dialog("open");
          return false;
        });

      });

    });
  </script>
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
<h2><%= Html.Encode(ViewData["Message"]) %></h2>

  <div style="margin-left: 15px; margin-right: auto;">
    <%int idx = 0;%>
    <%foreach (var segment in ((IEnumerable<Segment>)ViewData["Segments"]))
      { %>
    <%idx++; %>
    <div class="span-6 segment-container">
      <div class="box">
        <div class="top">
          <span></span>
        </div>
        <div class="box-content">
          <div class="box-content2">
            <div class="box-padding-light">
              <div class="item">
                <div class="column segment">
                  <div class="image">
                    <a class="segment" href="<%="http://localhost/BookTvReminder/home/segmentdetail/" + (segment.Title + segment.Day + segment.Time).GetHashCode()%>"
                      title="<%: segment.Title %>" rel="<%="http://localhost/BookTvReminder/home/segmentdetail/" + (segment.Title + segment.Day + segment.Time).GetHashCode()%>">
                      <img src="<%= segment.ImageUrl%>" alt="<%: segment.Title %>" />
                    </a>
                  </div>
                  <div class="source-icon">
                    <a href="<%= segment.SegmentDetailUrl%>" title="View on BookTV">
                      <img src="<%= Url.Content ("~/images/BTVTest.gif") %>" alt="BookTV" />
                    </a>
                  </div>
                  <div id="<%="header" + idx%>" class="title">
                    <%=segment.Title.SubstringOrDefault(0,75,"...") %>
                  </div>
                  <div class="ajax-dialog">
                    <img style="" src="<%= Url.Content("~/images/ajax-loader.gif")%>" alt="Segment detail loading..." /></div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="bottom">
          <span></span>
        </div>
      </div>
    </div>
    <% } %>
  </div>

  <div class="clear">
  </div>

</asp:Content>
