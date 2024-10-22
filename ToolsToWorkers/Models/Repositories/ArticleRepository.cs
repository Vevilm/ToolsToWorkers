using Microsoft.EntityFrameworkCore;
using ToolsToWorkers.Data;
using ToolsToWorkers.Data.RepositoryInterfaces;
using ToolsToWorkers.Models.Views;

namespace ToolsToWorkers.Models.Repositories
{
    public class ArticleRepository : IArticlesRepository
    {
        private readonly ApplicationDBContext _context;

        public ArticleRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public bool Add(Article article)
        {
            _context.Articles.Add(article);
            return Save();
        }

        public async Task<IEnumerable<Article>> GetAll()
        {
            return await _context.Articles.ToListAsync();
        }

        public async Task<IQueryable<Article>> GetAllDBSet()
        {
            return _context.Articles;
        }

        public async Task<Article> GetByIDAsync(string id)
        {
            var article = await _context.Articles.FirstOrDefaultAsync(a => a.ID == id);
            return article;
        }

        public async Task<Article> GetByIDAsyncNoTracking(string id)
        {
            var article = await _context.Articles.AsNoTracking().FirstOrDefaultAsync(a => a.ID == id);
            return article;
        }

        public bool Save()
        {
            var result = _context.SaveChanges();
            return result > 0;
        }

        public async Task<IEnumerable<Article>> SearchByID(string id, IQueryable<Article> articles)
        {
            List<Article> result = new List<Article>();
            if (id != null)
            {
                result = await (articles ?? _context.Articles.AsNoTracking()).Where(a => a.ID.Contains(id)).ToListAsync();
            }
            else
            {
                return await(articles ?? _context.Articles.AsNoTracking()).ToListAsync();
            }
            if (result == null) return await(articles ?? _context.Articles.AsNoTracking()).ToListAsync();
            else return result;
        }

        public async Task<IEnumerable<Article>> SearchByName(string name, IQueryable<Article> articles)
        {
            List<Article> result = new List<Article>();
            if (name != null)
            {
                result = await (articles ?? _context.Articles.AsNoTracking()).Where(a => a.Name.Contains(name)).ToListAsync();
            }
            else
            {
                return await(articles ?? _context.Articles.AsNoTracking()).ToListAsync();
            }
            if (result == null) return await(articles ?? _context.Articles.AsNoTracking()).ToListAsync();
            else return result;
        }

        public bool Update(Article article)
        {
            _context.Update(article);
            return Save();
        }

        public bool ArticleTaken(string article)
        {
            var user = _context.Articles.FirstOrDefault(a => a.ID == article);
            return user != null;
        }

        public async Task<IEnumerable<Article>> GetSlice(int count, int elementsPerPage, IQueryable<Article> articles)
        {
            return await articles.Skip((count - 1) * elementsPerPage).Take(elementsPerPage).ToListAsync();
        }

        public async Task<IQueryable<Article>> SearchByIDQuery(string id)
        {
            IQueryable<Article> result;
            if (id != null)
            {
                result = _context.Articles.AsNoTracking().Where(a => a.ID.Contains(id));
            }
            else
            {
                return _context.Articles.AsNoTracking();
            }
            if (result == null) return _context.Articles.AsNoTracking();
            else return result;
        }

        public async Task<IQueryable<Article>> SearchByNameQuery(string name)
        {
            IQueryable<Article> result;
            if (name != null)
            {
                result = _context.Articles.AsNoTracking().Where(a => a.Name.Contains(name));
            }
            else
            {
                return _context.Articles.AsNoTracking();
            }
            if (result == null) return _context.Articles.AsNoTracking();
            else return result;
        }
    }
}
