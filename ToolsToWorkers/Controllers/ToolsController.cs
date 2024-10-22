using Microsoft.AspNetCore.Mvc;
using ToolsToWorkers.Data.enums;
using ToolsToWorkers.Data.RepositoryInterfaces;
using ToolsToWorkers.Data.SearchData;
using ToolsToWorkers.Models;
using ToolsToWorkers.Models.Views;

namespace ToolsToWorkers.Controllers
{
    public class ToolsController : Controller
    {
        private readonly IToolRepository repository;
        private readonly IToolRequestRepository requestRepository;

        public ToolsController(IToolRepository repository, IToolRequestRepository requestRepository)
        {
            this.repository = repository;
            this.requestRepository = requestRepository;

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
                if (!ModelState.IsValid || tool.StorageID <= 0 || !repository.ArticleTaken(tool.ArticleID))
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
            if (!ModelState.IsValid || tool.StorageID <= 0 || !repository.ArticleTaken(tool.ArticleID))
            {
                return View(tool);
            }
            repository.Add(tool);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index(int page = 1, string searchDataStr = "")
        {
            ToolSearchData searchData = ToolSearchData.GetSearchData(searchDataStr);
            var toolsViews = FilterByStatus(await repository.GetAllDBSet(), searchData.Status);
            searchData.Tools = await repository.GetSlice(searchData.PageNumber, searchData.PageSize, await GetTools(searchData, toolsViews));
            searchData.elementsCount = GetTools(searchData, toolsViews).Result.Count();
            return View(searchData);
        }
        [HttpPost]
        public async Task<IActionResult> Index(ToolSearchData searchData)
        {
            var toolsViews = FilterByStatus(await repository.GetAllDBSet(), searchData.Status);
            searchData.Tools = await repository.GetSlice(searchData.PageNumber, searchData.PageSize, await GetTools(searchData, toolsViews));
            searchData.elementsCount = GetTools(searchData, toolsViews).Result.Count();
            return View(searchData);
        }

        async Task<IQueryable<ToolsView>> GetTools(ToolSearchData searchData, IQueryable<ToolsView> filtered)
        {
            switch (searchData.Field)
            {
                case "Название":
                    return await repository.SearchByNameQuery(searchData.Value, filtered);
                    break;
                case "Склад":
                    return await repository.SearchByStorageQuery(searchData.Value, filtered);
                    break;
            }
            return await repository.GetAllDBSet();
        }

        public async Task<IActionResult> ChooseWorker(int ID)
        {
            ToolRequest toolRequest = new ToolRequest();
            toolRequest.ToolID = ID;
            return View(toolRequest);
        }
        [HttpPost]
        public async Task<IActionResult> ChooseWorker(ToolRequest toolRequest)
        {
            if (toolRequest.WorkerID == 0)
                return View(toolRequest);

            toolRequest.ToolID = toolRequest.ID;
            toolRequest.ID = 0;
            toolRequest.Requested = DateTime.Now;
            toolRequest.Status = ToolRequestStatus.Запрошен.ToString();
            toolRequest.Returned = DateTime.Now.AddYears(-1);
            
            requestRepository.Add(toolRequest);
            Tool tool = await repository.GetToolByIDAsyncNoTracking(toolRequest.ToolID);
            tool.Status = ToolStatus.Занят.ToString();
            repository.Update(tool);

            return RedirectToAction("Index");
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
