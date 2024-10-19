using System.ComponentModel.DataAnnotations;

namespace ToolsToWorkers.Models
{
    public class Storage
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }

        public Storage() { }

        public Storage(string name, string type, string status)
        {
            Name = name;
            Type = type;
            Status = status;
        }
    }
}
