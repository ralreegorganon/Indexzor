using System.Collections.Generic;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Snowball;
using Lucene.Net.Documents;
using Lucene.Net.Highlight;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;

namespace Indexzor.Common
{
    public class DocumentIndexSearcher
    {
        private readonly string indexPath;

        public DocumentIndexSearcher(string indexPath)
        {
            this.indexPath = indexPath;
        }

        public IEnumerable<SearchResult> Search(string query)
        {
            Analyzer analyzer = new SnowballAnalyzer("English");
            QueryParser parser = new QueryParser("text", analyzer);
            Query luceneQuery = parser.Parse(query);
            Directory directory = FSDirectory.GetDirectory(indexPath);
            IndexSearcher searcher = new IndexSearcher(directory);


            QueryScorer queryScorer = new QueryScorer(luceneQuery);
            Highlighter highlighter = new Highlighter(queryScorer);


            TopDocs topDocs = searcher.Search(luceneQuery, 100);

            var searchResults = new List<SearchResult>();
            foreach (ScoreDoc scoreDoc in topDocs.scoreDocs)
            {
                Document doc = searcher.Doc(scoreDoc.doc);
                searchResults.Add(new SearchResult { Path = doc.Get("path"), Score = scoreDoc.score, Title = doc.Get("title"), Preview = highlighter.GetBestFragment(analyzer, "text", doc.Get("text")) });
            }

            return searchResults;
        }
    }
}
