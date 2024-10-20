using ToolsToWorkers.Models;
using ToolsToWorkers.Models.Views;

namespace ToolsToWorkers.Data.SearchData
{
    public class ArticleSearchData
    {
        public IEnumerable<Article> Articles { get; set; }
        public string Value { get; set; } = "";
        public string Field { get; set; } = "";
    }
}
