using System.ComponentModel.DataAnnotations;

namespace ToolsToWorkers.Models
{
    public class Article
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description {  get; set; }
        public double Weight { get; set; }

        public Article() { }

        public Article(string iD, string name, string description, double weight)
        {
            ID = iD;
            Name = name;
            Description = description;
            Weight = weight;
        }
    }
}
