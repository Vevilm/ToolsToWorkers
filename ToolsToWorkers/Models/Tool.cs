using System.ComponentModel.DataAnnotations;

namespace ToolsToWorkers.Models
{
    public class Tool
    {
        [Key]
        public int ID { get; set; }
        public int StorageID { get; set; }
        public string ArticleID { get; set; }
        public string Status { get; set; }

        public Tool() { }

        public Tool(int storageID, string articleID, string status)
        {
            StorageID = storageID;
            ArticleID = articleID;
            Status = status;
        }
    }
}
