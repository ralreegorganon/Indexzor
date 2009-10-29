<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SearchResultsViewData>" %>
<%@ Import Namespace="Indexzor.Models"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SearchResults
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <span><%= Model.SearchResults.Count %> records found</span>
    <br />
    <% foreach (var searchResult in Model.SearchResults) { %>
        <span><%= searchResult.Title %>: <em><%= searchResult.Path %></em></span>            
    <% } %>
</asp:Content>
