using Microsoft.AspNetCore.Mvc;
using ToolsToWorkers.Data.RepositoryInterfaces;
using ToolsToWorkers.Data.SearchData;
using ToolsToWorkers.Models;
using ToolsToWorkers.Models.Views;

namespace ToolsToWorkers.Controllers
{
    public class ToolRequestsController : Controller
    {
        private readonly IToolRequestRepository repository;

        public ToolRequestsController(IToolRequestRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> Edit(int ID)
        {
            var toolRequest = await repository.GetRequestByIDAsyncNoTracking(ID);
            return View(toolRequest);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ToolRequest toolRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(toolRequest);
                }
                repository.Update(toolRequest);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

            }
            return View(toolRequest);
        }

        public async Task<IActionResult> Details(int id)
        {
            ToolRequestsView toolRequestsView = await repository.GetByIDAsync(id);
            return View(toolRequestsView);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ToolRequest toolRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(toolRequest);
            }
            repository.Add(toolRequest);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ToolRequestsView> toolRequests = await repository.GetAll();
            ToolRequestSearchData searchData = new ToolRequestSearchData();
            searchData.ToolRequests = toolRequests;
            return View(searchData);
        }
        [HttpPost]
        public async Task<IActionResult> Index(ToolRequestSearchData searchData)
        {
            var toolRequests = FilterByStatus(await repository.GetAllDBSet(), searchData.Status);
            switch (searchData.Field)
            {
                case "Логин":
                    searchData.ToolRequests = await repository.SearchByLogin(searchData.Value, toolRequests);
                    break;
                case "Название":
                    searchData.ToolRequests = await repository.SearchByName(searchData.Value, toolRequests);
                    break;
            }
            return View(searchData);
        }

        private IQueryable<ToolRequestsView> FilterByStatus(IQueryable<ToolRequestsView> toolRequests, string status)
        {
            switch (status)
            {
                case "Все":
                    return toolRequests;
                default:
                    return toolRequests.Where(x => x.RequestStatus == status);
            }
        }
    }
}
