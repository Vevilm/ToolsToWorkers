using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using ToolsToWorkers.Data;
using ToolsToWorkers.Data.RepositoryInterfaces;

namespace ToolsToWorkers.Models.Repositories
{
    public class UserRepository : IUsersRepository
    {
        private readonly ApplicationDBContext _context;
        
        public UserRepository(ApplicationDBContext context)
        {
            _context = context;    
        }

        public IQueryable<User> FilterByRole(string role, IQueryable<User> users = null)
        {
            IQueryable<User> filtered = (users ?? _context.Users).Where(a => a.Role == role);
            return filtered;
        }

        public IQueryable<User> FilterByStatus(string status, IQueryable<User> users = null)
        {
            IQueryable<User> filtered = (users ?? _context.Users).Where(a => a.Status == status);
            return filtered;
        }

        public async Task<IEnumerable<User>> SearchByLogin(string login, IQueryable<User> users)
        {
            List<User> result = new List<User>();
            if (login != null)
            {
                result = await (users ?? _context.Users.AsNoTracking()).Where(a => a.Login.Contains(login)).ToListAsync();
            }
            else
            {
                 return await (users ?? _context.Users.AsNoTracking()).ToListAsync();
            }
            if (result == null) return await (users ?? _context.Users.AsNoTracking()).ToListAsync();
            else return result;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }
        
        public async Task<IQueryable<User>> GetAllDBSet()
        {
            return _context.Users;
        }

        public async Task<User> GetByIDAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(a => a.ID == id);
            return user;
        }

        public async Task<User> GetByIDAsyncNoTracking(int id)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(a => a.ID == id);
            return user;
        }

        public bool Add(User employee)
        {
            _context.Users.Add(employee);
            return Save();
        }

        public bool Save()
        {
            var result = _context.SaveChanges();
            return result > 0;
        }

        public bool Update(User employee)
        {
            _context.Update(employee);
            return Save();
        }

        public bool LoginTaken(string login)
        {
            var user = _context.Users.FirstOrDefault(a => a.Login == login);
            return user != null;
        }
    }
}
