using Microsoft.EntityFrameworkCore;
using System.Data;
using ToolsToWorkers.Data;
using ToolsToWorkers.Data.RepositoryInterfaces;

namespace ToolsToWorkers.Models.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        private readonly ApplicationDBContext _context;

        public StorageRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public bool Add(Storage storage)
        {
            _context.Storages.Add(storage);
            return Save();
        }

        public IQueryable<Storage> FilterByStatus(string status, IQueryable<Storage> storages)
        {
            IQueryable<Storage> filtered = (storages ?? _context.Storages).Where(a => a.Status == status);
            return filtered;
        }

        public IQueryable<Storage> FilterByType(string type, IQueryable<Storage> storages)
        {
            IQueryable<Storage> filtered = (storages ?? _context.Storages).Where(a => a.Type == type);
            return filtered;
        }

        public async Task<IEnumerable<Storage>> GetAll()
        {
            return await _context.Storages.ToListAsync();
        }

        public async Task<IQueryable<Storage>> GetAllDBSet()
        {
            return _context.Storages;
        }

        public async Task<Storage> GetByIDAsync(int id)
        {
            var storage = await _context.Storages.FirstOrDefaultAsync(a => a.ID == id);
            return storage;
        }

        public async Task<Storage> GetByIDAsyncNoTracking(int id)
        {
            var storage = await _context.Storages.AsNoTracking().FirstOrDefaultAsync(a => a.ID == id);
            return storage;
        }

        public bool Save()
        {
            var result = _context.SaveChanges();
            return result > 0;
        }

        public async Task<IEnumerable<Storage>> SearchByName(string name, IQueryable<Storage> storages)
        {
            List<Storage> result = new List<Storage>();
            if (name != null)
            {
                result = await (storages ?? _context.Storages.AsNoTracking()).Where(a => a.Name.Contains(name)).ToListAsync();
            }
            else
            {
                return await (storages ?? _context.Storages.AsNoTracking()).ToListAsync();
            }
            if (result == null) return await (storages ?? _context.Storages.AsNoTracking()).ToListAsync();
            else return result;
        }

        public bool Update(Storage storage)
        {
            _context.Update(storage);
            return Save();
        }
    }
}
