﻿using Microsoft.EntityFrameworkCore;
using System.Data;
using ToolsToWorkers.Data;
using ToolsToWorkers.Data.RepositoryInterfaces;
using ToolsToWorkers.Models;
using ToolsToWorkers.Models.Views;
namespace ToolsToWorkers.Models.Repositories
{
    public class ToolRepository : IToolRepository
    {
        private readonly ApplicationDBContext _context;

        public ToolRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public bool Add(Tool tool)
        {
            _context.Tools.Add(tool);
            return Save();
        }

        public IQueryable<ToolsView> FilterByStatus(string status, IQueryable<ToolsView> toolsViews)
        {
            IQueryable<ToolsView> filtered = (toolsViews ?? _context.toolsView).Where(a => a.Status == status);
            return filtered;
        }

        public async Task<IEnumerable<ToolsView>> GetAll()
        {
            return await _context.toolsView.ToListAsync();
        }

        public async Task<IQueryable<ToolsView>> GetAllDBSet()
        {
            return _context.toolsView;
        }

        public async Task<ToolsView> GetByIDAsync(int id)
        {
            var tool = await _context.toolsView.FirstOrDefaultAsync(a => a.ID == id);
            return tool;
        }

        public async Task<ToolsView> GetByIDAsyncNoTracking(int id)
        {
            var tool = await _context.toolsView.AsNoTracking().FirstOrDefaultAsync(a => a.ID == id);
            return tool;
        }

        public async Task<IEnumerable<ToolsView>> SearchByName(string name, IQueryable<ToolsView> toolsViews)
        {
            List<ToolsView> result = new List<ToolsView>();
            if (name != null)
            {
                result = await (toolsViews ?? _context.toolsView.AsNoTracking()).Where(a => a.Name.Contains(name)).ToListAsync();
            }
            else
            {
                return await (toolsViews ?? _context.toolsView.AsNoTracking()).ToListAsync();
            }
            if (result == null) return await (toolsViews ?? _context.toolsView.AsNoTracking()).ToListAsync();
            else return result;
        }

        public async Task<IEnumerable<ToolsView>> SearchByStorage(string storage, IQueryable<ToolsView> toolsViews)
        {
            List<ToolsView> result = new List<ToolsView>();
            if (storage != null)
            {
                result = await (toolsViews ?? _context.toolsView.AsNoTracking()).Where(a => a.Storage.Contains(storage)).ToListAsync();
            }
            else
            {
                return await (toolsViews ?? _context.toolsView.AsNoTracking()).ToListAsync();
            }
            if (result == null) return await (toolsViews ?? _context.toolsView.AsNoTracking()).ToListAsync();
            else return result;
        }

        public bool Save()
        {
            var result = _context.SaveChanges();
            return result > 0;
        }

        public bool Update(Tool tool)
        {
            _context.Update(tool);
            return Save();
        }

        public async Task<Tool> GetToolByIDAsyncNoTracking(int id)
        {
            var tool = await _context.Tools.AsNoTracking().FirstOrDefaultAsync(a => a.ID == id);
            return tool;
        }

        public bool ArticleTaken(string article)
        {
            var user = _context.Articles.FirstOrDefault(a => a.ID == article);
            return user != null;
        }

        public bool StorageTaken(int storage)
        {
            var user = _context.Storages.FirstOrDefault(a => a.ID == storage);
            return user != null;
        }

        public bool WorkerIdTaken(int WorkerID)
        {
            var user = _context.Users.FirstOrDefault(a => a.ID == WorkerID);
            return user != null;
        }

        public async Task<IEnumerable<ToolsView>> GetSlice(int count, int elementsPerPage, IQueryable<ToolsView> toolViews)
        {
            return await toolViews.Skip((count - 1) * elementsPerPage).Take(elementsPerPage).ToListAsync();
        }

        public async Task<IQueryable<ToolsView>> SearchByNameQuery(string name, IQueryable<ToolsView> toolsViews)
        {
            IQueryable<ToolsView> result;
            if (name != null)
            {
                result = (toolsViews ?? _context.toolsView.AsNoTracking()).Where(a => a.Name.Contains(name));
            }
            else
            {
                return (toolsViews ?? _context.toolsView.AsNoTracking());
            }
            if (result == null) return (toolsViews ?? _context.toolsView.AsNoTracking());
            else return result;
        }

        public async Task<IQueryable<ToolsView>> SearchByStorageQuery(string storage, IQueryable<ToolsView> toolsViews)
        {
            IQueryable<ToolsView> result;
            if (storage != null)
            {
                result = (toolsViews ?? _context.toolsView.AsNoTracking()).Where(a => a.Storage.Contains(storage));
            }
            else
            {
                return (toolsViews ?? _context.toolsView.AsNoTracking());
            }
            if (result == null) return (toolsViews ?? _context.toolsView.AsNoTracking());
            else return result;
        }
    }
}
