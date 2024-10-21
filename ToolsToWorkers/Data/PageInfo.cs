namespace ToolsToWorkers.Data
{
    public class PageInfo
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 3;
        public int TotalItems { get; set; } 
        public int TotalPages { get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }}
    }
}
