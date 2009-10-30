using System.Configuration;
using System.Web.Mvc;
using Indexzor.Common;
using Indexzor.Models;

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
            DocumentIndexSearcher documentIndexSearcher = new DocumentIndexSearcher(Server.MapPath(ConfigurationManager.AppSettings["IndexDirectory"]));
            return View("SearchResults", new SearchResultsViewData {SearchResults = documentIndexSearcher.Search(query)});
        }
    }
}
