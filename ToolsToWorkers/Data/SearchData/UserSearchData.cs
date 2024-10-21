using ToolsToWorkers.Models;

namespace ToolsToWorkers.Data.SearchData
{
    public class UserSearchData
    {
        public IEnumerable<User> Users { get; set; }
        public string Login { get; set; } = "";
        public string Role { get; set; } = "Все";
        public string Status { get; set; } = "Все";

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 3;
        public int TotalItems { get; set; }
        public int TotalPages { get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); } }
        public UserSearchData() { }
        public UserSearchData(UserSearchData userSearchData, int i)
        {
            Users = userSearchData.Users;
            Login = userSearchData.Login;
            Role = userSearchData.Role;
            Status = userSearchData.Status;

        } 
    }
}
