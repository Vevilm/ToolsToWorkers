namespace ToolsToWorkers.Data.SearchData
{
    public class SearchData
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 3;
        public int elementsCount { get; set; }
        public int TotalPages { get { return (int)Math.Ceiling((decimal)elementsCount / PageSize); } }
        public bool HasPreviousPage { get { return (PageNumber > 1); } }
        public bool HasNextPage { get { return (PageNumber < TotalPages); } }
    }
}
