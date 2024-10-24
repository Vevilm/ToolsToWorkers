﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToolsDistribution.Data;
using ToolsToWorkers.Data.RepositoryInterfaces;
using ToolsToWorkers.Data.SearchData;
using ToolsToWorkers.Models;
using ToolsToWorkers.Models.Repositories;
using ToolsToWorkers.Data;

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
                if (users.Count() >= 1 && LoginComparison(users, user))
                {
                    MessegaMarkers.InvalidLogin = true;
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

        private bool LoginComparison(IEnumerable<User> users, User user)
        {
            foreach (var item in users)
            {
                if (item.ID != user.ID)
                {
                    return true;
                }
            }
            return false;
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
                if (repository.LoginTaken(user.Login))
                {
                    MessegaMarkers.InvalidLogin = true;
                }
                return View(user);
            }
            user.HashedPassword = PasswordHasher.Generate(user.HashedPassword);
            repository.Add(user);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserSearchData searchData)
        {
            var users = FilterByRole(await repository.GetAllDBSet(), searchData.Role);
            users = FilterByStatus(users, searchData.Status);
            users = await repository.SearchByLoginQuery(searchData.Login, users);
            searchData.Users = await repository.GetSlice(searchData.PageNumber, searchData.PageSize, users);
            searchData.elementsCount = users.Count();
            return View(searchData);
        }
        public async Task<IActionResult> Index(int page = 1, string searchDataStr = "")
        {
            UserSearchData searchData = UserSearchData.GetSearchData(searchDataStr);
            var users = FilterByRole(await repository.GetAllDBSet(), searchData.Role);
            users = FilterByStatus(users, searchData.Status);
            searchData.Users = await repository.GetSlice(searchData.PageNumber, searchData.PageSize, await repository.SearchByLoginQuery(searchData.Login, users));
            searchData.elementsCount = repository.SearchByLoginQuery(searchData.Login, users).Result.Count();
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
