using Microsoft.EntityFrameworkCore;
using System.Data;
using ToolsToWorkers.Data;
using ToolsToWorkers.Data.RepositoryInterfaces;
using ToolsToWorkers.Models.Views;

namespace ToolsToWorkers.Models.Repositories
{
    public class ToolRequestRepository : IToolRequestRepository
    {
        private readonly ApplicationDBContext _context;

        public ToolRequestRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public bool Add(ToolRequest toolRequest)
        {
            _context.ToolRequests.Add(toolRequest);
            return Save();
        }

        public async Task<IQueryable<ToolRequestsView>> FilterByStatus(string status, IQueryable<ToolRequestsView> toolRequests)
        {
            IQueryable<ToolRequestsView> filtered = (toolRequests ?? _context.toolRequestsView).Where(a => a.RequestStatus == status);
            return filtered;
        }

        public async Task<IEnumerable<ToolRequestsView>> GetAll()
        {
            return await _context.toolRequestsView.ToListAsync();
        }

        public async Task<IQueryable<ToolRequestsView>> GetAllDBSet()
        {
            return _context.toolRequestsView;
        }

        public async Task<ToolRequestsView> GetByIDAsync(int id)
        {
            var toolRequest = await _context.toolRequestsView.FirstOrDefaultAsync(a => a.RequestID == id);
            return toolRequest;
        }

        public async Task<ToolRequestsView> GetByIDAsyncNoTracking(int id)
        {
            var toolRequest = await _context.toolRequestsView.AsNoTracking().FirstOrDefaultAsync(a => a.RequestID == id);
            return toolRequest;
        }

        public async Task<ToolRequest> GetRequestByIDAsyncNoTracking(int id)
        {
            var toolRequest = await _context.ToolRequests.AsNoTracking().FirstOrDefaultAsync(a => a.ID == id);
            return toolRequest;
        }

        public bool Save()
        {
            var result = _context.SaveChanges();
            return result > 0;
        }

        public async Task<IEnumerable<ToolRequestsView>> SearchByLogin(string login, IQueryable<ToolRequestsView> toolRequests)
        {
            List<ToolRequestsView> result = new List<ToolRequestsView>();
            if (login != null)
            {
                result = await (toolRequests ?? _context.toolRequestsView.AsNoTracking()).Where(a => a.UserLogin.Contains(login)).ToListAsync();
            }
            else
            {
                return await (toolRequests ?? _context.toolRequestsView.AsNoTracking()).ToListAsync();
            }
            if (result == null) return await (toolRequests ?? _context.toolRequestsView.AsNoTracking()).ToListAsync();
            else return result;
        }

        public async Task<IEnumerable<ToolRequestsView>> SearchByName(string name, IQueryable<ToolRequestsView> toolRequests)
        {
            List<ToolRequestsView> result = new List<ToolRequestsView>();
            if (name != null)
            {
                result = await (toolRequests ?? _context.toolRequestsView.AsNoTracking()).Where(a => a.ToolName.Contains(name)).ToListAsync();
            }
            else
            {
                return await (toolRequests ?? _context.toolRequestsView.AsNoTracking()).ToListAsync();
            }
            if (result == null) return await (toolRequests ?? _context.toolRequestsView.AsNoTracking()).ToListAsync();
            else return result;
        }

        public bool Update(ToolRequest toolRequest)
        {
            _context.Update(toolRequest);
            return Save();
        }
    }
}
