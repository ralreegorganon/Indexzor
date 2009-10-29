using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Indexzor.Common;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Snowball;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Directory=Lucene.Net.Store.Directory;

namespace Indexzor
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Search", action = "Index", id = "" }  // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);

            Directory directory = FSDirectory.GetDirectory(ConfigurationManager.AppSettings["IndexDirectory"],true);
            Analyzer analyzer = new SnowballAnalyzer("English");
            IndexWriter writer = new IndexWriter(directory, analyzer, true);

            new DirectoryInfo(ConfigurationManager.AppSettings["ContentDirectory"])
                .GetFiles()
                .Where(file => Parser.IsParseable(file.FullName))
                .Select(file => new { Path = file.FullName, Text = Parser.Parse(file.FullName), Title = file.Name })
                .ForEach((item) =>
                {
                    Document doc = new Document();
                    doc.Add(new Field("title", item.Title, Field.Store.YES, Field.Index.TOKENIZED));
                    doc.Add(new Field("path", item.Path, Field.Store.YES, Field.Index.NO));
                    doc.Add(new Field("text", item.Text, Field.Store.NO, Field.Index.TOKENIZED));
                    writer.AddDocument(doc);
                });

            writer.Optimize();
            writer.Close();
        }
    }
}