<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<BookTvReminder.Domain.Segment>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SegmentDetail
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>SegmentDetail</h2>

    <fieldset>
       <%-- <legend>Fields</legend>
        
        <div class="display-label">Series</div>
        <div class="display-field"><%: Model.Series %></div>
        
        <div class="display-label">Title</div>
        <div class="display-field"><%: Model.Title %></div>
        
        <div class="display-label">Description</div>
        <div class="display-field"><%= Model.Description %></div>
        
        <div class="display-label">AuthorNames</div>
        <div class="display-field"><%= Model.AuthorNames %></div>
       --%> 
        <ul >
              <li><%=Model.Author%></li>
              <li><%=Model.Day%></li>
              <li><%=Model.Time%></li>
              <li><%=Model.Duration%></li>   
              <li><%=Model.SegmentDetail.Series%></li>
              <li><%=Model.SegmentDetail.AuthorNames%></li>
              <li><%=Model.SegmentDetail.Title%></li>
              <li><%=Model.SegmentDetail.Description%></li>
              <li>
                <ul>
                <%foreach (var author in Model.SegmentDetail.Authors)
                  { %>
                <li><%=author.AuthorName %></li>
                <li><%=author.AuthorBio %></li>
                <li><%=author.ISBN %></li>            
                <% } %>
                </ul>
              </li>
              </ul>

    </fieldset>
<%--    <p>
        <%: Html.ActionLink("Edit", "Edit", new { /* id=Model.PrimaryKey */ }) %> |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>--%>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

