using ToolsToWorkers.Models;
using ToolsToWorkers.Models.Views;

namespace ToolsToWorkers.Data.SearchData
{
    public class ToolRequestSearchData
    {
        public IEnumerable<ToolRequestsView> ToolRequests { get; set; }
        public string Field { get; set; } = "";
        public string Value { get; set; } = "";
        public string Status { get; set; } = "Все";

    }
}
