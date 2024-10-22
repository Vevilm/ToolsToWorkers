using ToolsToWorkers.Models;
using ToolsToWorkers.Models.Views;

namespace ToolsToWorkers.Data.SearchData
{
    public class ToolRequestSearchData : SearchData
    {
        public IEnumerable<ToolRequestsView> ToolRequests { get; set; }
        public string Field { get; set; } = "";
        public string Value { get; set; } = "";
        public string Status { get; set; } = "Все";

        public string ToString(int i)
        {
            return Field + "/{}/" + Value + "/{}/" + Status + "/{}/" + i + "/{}/" + PageSize + "/{}/" + elementsCount;
        }
        public static ToolRequestSearchData GetSearchData(string sypher)
        {
            var data = sypher.Split("/{}/");
            ToolRequestSearchData result = new ToolRequestSearchData();
            if (data.Length > 1)
                result = new ToolRequestSearchData()
                {//return Login + "/{}/" + Role + "/{}/" + Status + "/{}/" + i + "/{}/" + PageSize + "/{}/" + elementsCount;
                    Field = data[0],
                    Value = data[1],
                    Status = data[2],
                    PageNumber = Convert.ToInt32(data[3]),
                    PageSize = Convert.ToInt32(data[4]),
                    elementsCount = Convert.ToInt32(data[5])
                };
            return result;
        }
    }
}
