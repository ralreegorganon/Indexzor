using System;
using System.IO;
using System.Linq;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Snowball;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Directory=Lucene.Net.Store.Directory;

namespace Indexzor.Common
{
    public class DocumentIndexBuilder
    {
        private readonly string indexPath;
        private readonly string contentPath;

        public DocumentIndexBuilder(string indexPath, string contentPath)
        {
            this.indexPath = indexPath;
            this.contentPath = contentPath;
        }

        public void Build()
        {
            Directory directory = FSDirectory.GetDirectory(indexPath);
            Analyzer analyzer = new SnowballAnalyzer("English");
            IndexWriter writer = new IndexWriter(directory, analyzer, true);

            new DirectoryInfo(contentPath)
                .GetFilesRecursive()
                .Where(file => Parser.IsParseable(file.FullName))
                .Select(file => new { Path = file.FullName, Text = Parser.Parse(file.FullName), Title = file.Name })
                .ForEach((item) =>
                {
                    Document doc = new Document();
                    doc.Add(new Field("title", item.Title, Field.Store.YES, Field.Index.TOKENIZED));
                    doc.Add(new Field("path", item.Path, Field.Store.YES, Field.Index.NO));
                    doc.Add(new Field("text", item.Text, Field.Store.YES, Field.Index.TOKENIZED));
                    writer.AddDocument(doc);
                });

            writer.Optimize();
            writer.Close();
        }
    }
}
