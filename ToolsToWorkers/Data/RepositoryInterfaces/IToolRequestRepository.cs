using ToolsToWorkers.Models;
using ToolsToWorkers.Models.Views;

namespace ToolsToWorkers.Data.RepositoryInterfaces
{
    public interface IToolRequestRepository
    {
        Task<IEnumerable<ToolRequestsView>> GetAll();

        Task<IQueryable<ToolRequestsView>> GetAllDBSet();

        Task<ToolRequestsView> GetByIDAsyncNoTracking(int id);

        Task<ToolRequest> GetRequestByIDAsyncNoTracking(int id);

        Task<ToolRequestsView> GetByIDAsync(int id);

        Task<IEnumerable<ToolRequestsView>> SearchByLogin(string login, IQueryable<ToolRequestsView> toolRequests);

        Task<IEnumerable<ToolRequestsView>> SearchByName(string name, IQueryable<ToolRequestsView> toolRequests);
        
        Task<IQueryable<ToolRequestsView>> FilterByStatus(string status, IQueryable<ToolRequestsView> toolRequests);

        bool Add(ToolRequest toolRequest);

        bool Update(ToolRequest toolRequest);

        bool Save();
    }
}
