﻿using ToolsToWorkers.Models;
using ToolsToWorkers.Models.Views;

namespace ToolsToWorkers.Data.RepositoryInterfaces
{
    public interface IToolRepository
    {
        Task<IEnumerable<ToolsView>> GetAll();

        Task<IQueryable<ToolsView>> GetAllDBSet();

        Task<ToolsView> GetByIDAsync(int id);

        Task<ToolsView> GetByIDAsyncNoTracking(int id);

        Task<Tool> GetToolByIDAsyncNoTracking(int id);

        Task<IEnumerable<ToolsView>> SearchByName(string name, IQueryable<ToolsView> toolsViews);

        Task<IEnumerable<ToolsView>> SearchByStorage(string storage, IQueryable<ToolsView> toolsViews);

        IQueryable<ToolsView> FilterByStatus(string status, IQueryable<ToolsView> toolsViews);

        bool Add(Tool tool);

        bool Update(Tool tool);

        bool Save();
    }
}