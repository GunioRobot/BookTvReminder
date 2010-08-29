<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Test
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

  <div class="container">
  	<div class="span-24">
  		The header
  	</div>

  	<div class="span-4">
  		The first column
  	</div>
  	<div class="span-16">
  		The center column
  	</div>
  	<div class="span-4 last">
  		The last column
  	</div>
	
  	<div class="span-24">
  		The footer
  	</div>
  </div>

</asp:Content>
