using ToolsToWorkers.Models;

namespace ToolsToWorkers.Data.SearchData
{
    public class UserSearchData
    {
        public IEnumerable<User> Users { get; set; }
        public string Login { get; set; } = "";
        public string Role { get; set; } = "Все";
        public string Status { get; set; } = "Все";
        public PageInfo pageInfo { get; set; }
        public UserSearchData() { }
        public UserSearchData(UserSearchData userSearchData, int i)
        {
            Users = userSearchData.Users;
            Login = userSearchData.Login;
            Role = userSearchData.Role;
            Status = userSearchData.Status;
            pageInfo = userSearchData.pageInfo;
            if (i == 0) pageInfo.PageNumber += 1;
            else if(i == -1) pageInfo.PageNumber -= 1;
            else pageInfo.PageNumber = i;

        } 
    }
}
