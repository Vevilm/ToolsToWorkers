using ToolsToWorkers.Data.RepositoryInterfaces;

namespace ToolsToWorkers.Models.Repositories
{
    public class ArticleRepository : IArticlesRepository
    {
        public bool Add(Article article)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Article>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Article>> GetAllDBSet()
        {
            throw new NotImplementedException();
        }

        public Task<Article> GetByIDAsync(string id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Article>> SearchByID(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Article>> SearchByName(string name)
        {
            throw new NotImplementedException();
        }

        public bool Update(Article article)
        {
            throw new NotImplementedException();
        }
    }
}
