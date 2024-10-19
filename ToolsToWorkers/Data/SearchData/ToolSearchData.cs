using ToolsToWorkers.Models;
using ToolsToWorkers.Models.Views;

namespace ToolsToWorkers.Data.SearchData
{
    public class ToolSearchData
    {
        public IEnumerable<ToolsView> Tools { get; set; }
        public string Value { get; set; } = "";
        public string Field { get; set; } = "";
        public string Status { get; set; } = "Все";
    }
}
