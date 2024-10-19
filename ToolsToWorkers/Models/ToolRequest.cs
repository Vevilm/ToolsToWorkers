using System.ComponentModel.DataAnnotations;

namespace ToolsToWorkers.Models
{
    public class ToolRequest
    {
        [Key]
        public int ID { get; set; }
        public int WorkerID { get; set; }
        public int ToolID { get; set; }
        public string Status { get; set; }
        public DateTime Requested { get; set; }
        public DateTime Returned { get; set; }

        public ToolRequest() { }

        public ToolRequest(int workerID, int  toolID, string status, DateTime requested)
        {
            WorkerID = workerID;
            ToolID = toolID;
            Status = status;
            Requested = requested;
            Returned = new DateTime(requested.Year - 1, requested.Month, requested.Day);
        }
    }
}
