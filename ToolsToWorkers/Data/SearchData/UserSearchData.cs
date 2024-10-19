using ToolsToWorkers.Models;

namespace ToolsToWorkers.Data.SearchData
{
    public class UserSearchData
    {
        public IEnumerable<User> Users { get; set; }
        public string Login { get; set; } = "";
        public string Role { get; set; } = "Все";
        public string Status { get; set; } = "Все";
    }
}
