<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SearchResultsViewData>" %>
<%@ Import Namespace="Indexzor.Models"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SearchResults
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div><%= Model.SearchResults.Count() %> records found</div>
    <% foreach (var searchResult in Model.SearchResults) { Html.RenderPartial("SearchResult", searchResult); } %>
</asp:Content>
