using ToolsToWorkers.Models;

namespace ToolsToWorkers.Data.SearchData
{
    public class StorageSearchData
    {
        public IEnumerable<Storage> Storages { get; set; }
        public string Name { get; set; } = "";
        public string Type { get; set; } = "Все";
        public string Status { get; set; } = "Все";
    }
}
