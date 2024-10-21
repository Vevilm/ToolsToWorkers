using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ToolsDistribution.Data;
using ToolsToWorkers.Data;
using ToolsToWorkers.Data.RepositoryInterfaces;
using ToolsToWorkers.Data.SearchData;
using ToolsToWorkers.Models;
using ToolsToWorkers.Models.Repositories;

namespace ToolsToWorkers.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersRepository repository;

        public UsersController(IUsersRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> Edit(int ID)
        {
            var user = await repository.GetByIDAsync(ID);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            try
            {
                if (!IsValidExeptPassword())
                {
                    return View(user);
                }
                var users = await repository.SearchByLogin(user.Login, null);
                if (users.Count() > 1)
                {
                    return View(user);
                }
                user.HashedPassword = repository.GetByIDAsyncNoTracking(user.ID).Result.HashedPassword;
                User result = new User(user.Surname, user.Name, user.Patronymic, user.Role, user.Status, user.Login, user.HashedPassword);
                result.ID = user.ID;
                repository.Update(result);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

            }
            return View(user);
        }

        private bool IsValidExeptPassword() {
            foreach (var item in ModelState)
            {
                if(item.Value.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid || item.Key == "HashedPassword")
                {
                    continue;
                }
                else return false;
            }
            return true;
        }

        public async Task<IActionResult> Details(int id)
        {
            User user = await repository.GetByIDAsync(id);
            return View(user);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (!ModelState.IsValid || repository.LoginTaken(user.Login))
            {
                return View(user);
            }
            user.HashedPassword = PasswordHasher.Generate(user.HashedPassword);
            repository.Add(user);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<User> users = await repository.GetSlice(1, 3, await repository.GetAllDBSet());
            UserSearchData searchData = new UserSearchData();
            searchData.Users = users;
            searchData.TotalItems = repository.GetAll().Result.Count();
            return View(searchData);
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserSearchData searchData)
        {
            var users = FilterByRole(await repository.GetAllDBSet(), searchData.Role);
            users = FilterByStatus(users, searchData.Status);
            var usersList = await repository.SearchByLogin(searchData.Login, users);
            searchData.Users = await repository.GetSlice(searchData.PageNumber, searchData.PageSize, usersList);
            searchData.TotalItems = repository.GetAll().Result.Count();
            // Сюда пагинацию
            return View(searchData);
        }


        private IQueryable<User> FilterByRole(IQueryable<User> users, string role)
        {
            switch (role)
            {
                case "Все":
                    return users;
                default:
                    return (IQueryable<User>)users.Where(x => x.Role == role);
            }
        }

        private IQueryable<User> FilterByStatus(IQueryable<User> users, string status)
        {
            switch (status)
            {
                case "Все":
                    return users;
                default:
                    return (IQueryable<User>)users.Where(x => x.Status == status);
            }
        }
    }
}
