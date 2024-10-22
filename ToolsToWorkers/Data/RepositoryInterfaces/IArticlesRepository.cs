using ToolsToWorkers.Models;

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

        public bool ArticleTaken(string article);

        Task<IEnumerable<Article>> GetSlice(int count, int elementsPerPage, IQueryable<Article> articles);

        Task<IQueryable<Article>> SearchByIDQuery(string name);

        Task<IQueryable<Article>> SearchByNameQuery(string name);
    }
}
