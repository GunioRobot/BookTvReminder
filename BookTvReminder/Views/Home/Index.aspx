<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="BookTvReminder.Domain" %>
<%@ Import Namespace="BookTvReminder.Domain.Utility" %>
<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">Home
  Page </asp:Content>
<asp:Content ID="indexHead" ContentPlaceHolderID="HeadContent" runat="server">
  <%--  <script type="text/javascript">
      // increase the default animation speed to exaggerate the effect
     // $.fx.speeds._default = 1000;

    $(document).ready(function () {

      $('div.header').each(function () {
        var $this = $(this);
        var $details = $this.siblings('div.details');

        //$this.click(function () {$details.dialog('open');});

        var config = {    
             over: function () {$details.dialog('open');}, // function = onMouseOver callback (REQUIRED)                 
             timeout: 500, // number = milliseconds delay before onMouseOut function is called
             out: function () {$details.dialog('close');}, // function = onMouseOut callback (REQUIRED)                
             interval: 500, // number = milliseconds delay before onMouseOver function is called
        };

        $this.hoverIntent(config);
      });

      $('div.details').dialog({
        autoOpen: false,
        show: 'fold',
        hide: 'clip', 
        modal: 'true',
        closeOnEscape: 'true',
        width: 400,
      });

    });
  </script>--%>
  <script type="text/javascript">
    // increase the default animation speed to exaggerate the effect
    // $.fx.speeds._default = 1000;

    $(document).ready(function () {

//      $('a.segment').cluetip({ width: 500 });
      $('a.segment').cluetip({        
        width: 500,        
        dropShadow: false,
        hoverIntent: true,
//        sticky: true,
        mouseOutClose: true,
        closePosition: 'title',
        closeText: '<img src="content/cross.png" alt="close" />'
      });

//      $('a.segment').cluetip({
//        width: 650,        
//        activation: click,                
//        closePosition: 'title',
//        closeText: '<img src="content/cross.png" alt="close" />'
//      });

    });
  </script>
  <style type="text/css">
    .block
    {
      height: 100px;
      overflow: hidden;
      padding: 10px;
      margin: 10px;
      border: 1px dashed whitesmoke;
    }
    div.title
    {
    }
  </style></asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server"><h2>
  <%= Html.Encode(ViewData["Message"]) %></h2>
  <%int idx = 0;%>
  <%foreach (var segment in ((IEnumerable<Segment>)ViewData["Segments"]))
    { %>
  <%idx++; %>
  <div class="span-5 block">
    <div style="float: left; padding-right: 10px;">
      <a class="segment" href="<%= segment.SegmentDetailUrl%>" title="Segment Details" rel="<%="http://localhost/BookTvReminder/home/segmentdetail/" + segment.Title.GetHashCode()%>">
        <img src="<%= segment.ImageUrl%>" alt="<%: segment.Title %>" />
      </a>    
    </div>
    <div id="<%="header" + idx%>" class="title">
      <%=segment.Title.SubstringOrDefault(0,75,"...") %>
    </div>    
      <div style=" position:relative;">
      <a href="<%= segment.SegmentDetailUrl%>" title="View on BookTV"  >
        <img src="Content/BTVTest.gif" height="16" width="16" alt="BookTV" style="position: absolute; bottom: 0; right: 0" />
      </a>
    </div>
  </div>

  <% } %>
</asp:Content>
