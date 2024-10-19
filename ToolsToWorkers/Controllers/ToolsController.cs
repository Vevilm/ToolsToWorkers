using Microsoft.AspNetCore.Mvc;
using ToolsToWorkers.Data.RepositoryInterfaces;
using ToolsToWorkers.Data.SearchData;
using ToolsToWorkers.Models;
using ToolsToWorkers.Models.Views;

namespace ToolsToWorkers.Controllers
{
    public class ToolsController : Controller
    {
        private readonly IToolRepository repository;

        public ToolsController(IToolRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> Edit(int ID)
        {
            var tool = await repository.GetToolByIDAsyncNoTracking(ID);
            return View(tool);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Tool tool)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(tool);
                }
                repository.Update(tool);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

            }
            return View(tool);
        }

        public async Task<IActionResult> Details(int id)
        {
            ToolsView toolsView = await repository.GetByIDAsync(id);
            return View(toolsView);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Tool tool)
        {
            if (!ModelState.IsValid)
            {
                return View(tool);
            }
            repository.Add(tool);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ToolsView> tools = await repository.GetAll();
            ToolSearchData searchData = new ToolSearchData();
            searchData.Tools = tools;
            return View(searchData);
        }
        [HttpPost]
        public async Task<IActionResult> Index(ToolSearchData searchData)
        {
            var toolsview = FilterByStatus(await repository.GetAllDBSet(), searchData.Status);
            switch (searchData.Field)
            {
                case "Название":
                    searchData.Tools = await repository.SearchByName(searchData.Value, toolsview);
                    break;
                case "Склад":
                    searchData.Tools = await repository.SearchByStorage(searchData.Value, toolsview);
                    break;
            }
            return View(searchData);
        }

        private IQueryable<ToolsView> FilterByStatus(IQueryable<ToolsView> toolsViews, string status)
        {
            switch (status)
            {
                case "Все":
                    return toolsViews;
                default:
                    return toolsViews.Where(x => x.Status == status);
            }
        }
    }
}
