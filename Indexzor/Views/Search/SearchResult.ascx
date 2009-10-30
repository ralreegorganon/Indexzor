<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Indexzor.Common.SearchResult>" %>

<div>
    <div><a href="<%= Model.Path %>"><%= Model.Title%></a></div>
    <div><%= Model.Preview%></div>
</div>  