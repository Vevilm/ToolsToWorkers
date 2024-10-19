using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ToolsToWorkers.Models;

namespace ToolsToWorkers.Data.RepositoryInterfaces
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetAll();

        Task<IQueryable<User>> GetAllDBSet();

        Task<User> GetByIDAsync(int id);

        Task<User> GetByIDAsyncNoTracking(int id);
        
        Task<IEnumerable<User>> SearchByLogin(string login, IQueryable<User> users);

        IQueryable<User> FilterByStatus(string status, IQueryable<User> users);

        IQueryable<User> FilterByRole(string role, IQueryable<User> users);

        bool LoginTaken(string login);

        bool Add(User employee);

        bool Update(User employee);

        bool Save();
    }
}
