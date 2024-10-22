using ToolsToWorkers.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ToolsToWorkers.Data.SearchData
{
    public class UserSearchData : SearchData
    {
        public IEnumerable<User> Users { get; set; }
        public string Login { get; set; } = "";
        public string Role { get; set; } = "Все";
        public string Status { get; set; } = "Все";

        public string ToString(int i)
        {
            return Login + "/{}/" + Role + "/{}/" + Status + "/{}/" + i + "/{}/" + PageSize + "/{}/" + elementsCount;
        }
        public static UserSearchData GetSearchData(string sypher)
        {
            var data = sypher.Split("/{}/");
            UserSearchData result = new UserSearchData();
            if (data.Length > 1)
                result = new UserSearchData()
                {//return Login + "/{}/" + Role + "/{}/" + Status + "/{}/" + i + "/{}/" + PageSize + "/{}/" + elementsCount;
                    Login = data[0],
                    Role = data[1],
                    Status = data[2],
                    PageNumber = Convert.ToInt32(data[3]),
                    PageSize = Convert.ToInt32(data[4]),
                    elementsCount = Convert.ToInt32(data[5])
                };
            return result;
        }
    }
}
