using Microsoft.AspNetCore.Mvc;
using ToolsToWorkers.Data.RepositoryInterfaces;
using ToolsToWorkers.Data.SearchData;
using ToolsToWorkers.Models;

namespace ToolsToWorkers.Controllers
{
    public class StoragesController : Controller
    {
        private readonly IStorageRepository repository;

        public StoragesController(IStorageRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> Edit(int ID)
        {
            var storage = await repository.GetByIDAsync(ID);
            return View(storage);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Storage storage)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(storage);
                }
                repository.Update(storage);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

            }
            return View(storage);
        }

        public async Task<IActionResult> Details(int id)
        {
            Storage storage = await repository.GetByIDAsync(id);
            return View(storage);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Storage storage)
        {
            if (!ModelState.IsValid)
            {
                return View(storage);
            }
            repository.Add(storage);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Storage> storages = await repository.GetAll();
            StorageSearchData searchData = new StorageSearchData();
            searchData.Storages = storages;
            return View(searchData);
        }
        [HttpPost]
        public async Task<IActionResult> Index(StorageSearchData searchData)
        {
            var users = FilterByType(await repository.GetAllDBSet(), searchData.Type);
            users = FilterByStatus(users, searchData.Status);
            searchData.Storages = await repository.SearchByName(searchData.Name, users);
            return View(searchData);
        }

        private IQueryable<Storage> FilterByType(IQueryable<Storage> storages, string type)
        {
            switch (type)
            {
                case "Все":
                    return storages;
                default:
                    return storages.Where(x => x.Type == type);
            }
        }

        private IQueryable<Storage> FilterByStatus(IQueryable<Storage> storages, string status)
        {
            switch (status)
            {
                case "Все":
                    return storages;
                default:
                    return storages.Where(x => x.Status == status);
            }
        }
    }
}
