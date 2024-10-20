﻿using ToolsToWorkers.Models;

namespace ToolsToWorkers.Data.RepositoryInterfaces
{
    public interface IArticlesRepository
    {
        Task<IEnumerable<Article>> GetAll();

        Task<IQueryable<Article>> GetAllDBSet();

        Task<Article> GetByIDAsync(string id);

        Task<IEnumerable<Article>> SearchByName(string name, IQueryable<Article> articles);

        Task<IEnumerable<Article>> SearchByID(string id, IQueryable<Article> articles);

        Task<Article> GetByIDAsyncNoTracking(string id);

        bool Add(Article article);

        bool Update(Article article);

        bool Save();
    }
}
