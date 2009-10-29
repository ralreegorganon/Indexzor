using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Indexzor.Models
{
    public class SearchResult
    {
        public string Title { get; set; }
        public string Path { get; set; }
        public float Score { get; set; }
    }
}
