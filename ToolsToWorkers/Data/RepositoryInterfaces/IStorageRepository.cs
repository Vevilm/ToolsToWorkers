using ToolsToWorkers.Models;

namespace ToolsToWorkers.Data.RepositoryInterfaces
{
    public interface IStorageRepository
    {
        Task<IEnumerable<Storage>> GetAll();

        Task<IQueryable<Storage>> GetAllDBSet();

        Task<Storage> GetByIDAsync(int id);

        Task<Storage> GetByIDAsyncNoTracking(int id);

        Task<IEnumerable<Storage>> SearchByName(string name, IQueryable<Storage> storages);

        IQueryable<Storage> FilterByType(string type, IQueryable<Storage> storages);

        IQueryable<Storage> FilterByStatus(string status, IQueryable<Storage> storages);

        bool Add(Storage storage);

        bool Update(Storage storage);

        bool Save();

        Task<IEnumerable<Storage>> GetSlice(int count, int elementsPerPage, IQueryable<Storage> storages);

        IQueryable<Storage> SearchByNameQuery(string login, IQueryable<Storage> storages);
    }
}
