using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Indexzor.Models;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Snowball;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;

namespace Indexzor.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(string query)
        {
            Analyzer analyzer = new SnowballAnalyzer("English");
            QueryParser parser = new QueryParser("text", analyzer);
            Query luceneQuery = parser.Parse(query);
            Directory directory = FSDirectory.GetDirectory(ConfigurationManager.AppSettings["IndexDirectory"]);
            IndexSearcher searcher = new IndexSearcher(directory);
            Hits hits = searcher.Search(luceneQuery);

            var searchResults = new List<SearchResult>();

            int results = hits.Length();
            for (int i = 0; i < results; i++)
            {
                Document doc = hits.Doc(i);
                searchResults.Add(new SearchResult { Path = doc.Get("path"), Score = hits.Score(i), Title = doc.Get("title") });
            }

            var searchResultsViewData = new SearchResultsViewData {SearchResults = searchResults};

            return View("SearchResults", searchResultsViewData);
        }

    }
}
