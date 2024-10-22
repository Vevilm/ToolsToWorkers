using System.Data;
using ToolsToWorkers.Models;
using ToolsToWorkers.Models.Views;

namespace ToolsToWorkers.Data.SearchData
{
    public class ArticleSearchData : SearchData
    {
        public IEnumerable<Article> Articles { get; set; }
        public string Value { get; set; } = "";
        public string Field { get; set; } = "";

        public string ToString(int i)
        {
            return Value + "/{}/" + Field + "/{}/" + i + "/{}/" + PageSize + "/{}/" + elementsCount;
        }
        public static ArticleSearchData GetSearchData(string sypher)
        {
            var data = sypher.Split("/{}/");
            ArticleSearchData result = new ArticleSearchData();
            if (data.Length > 1)
                result = new ArticleSearchData()
                {//return Login + "/{}/" + Role + "/{}/" + Status + "/{}/" + i + "/{}/" + PageSize + "/{}/" + elementsCount;
                    Value = data[0],
                    Field = data[1],
                    PageNumber = Convert.ToInt32(data[2]),
                    PageSize = Convert.ToInt32(data[3]),
                    elementsCount = Convert.ToInt32(data[4])
                };
            return result;
        }
    }
}
