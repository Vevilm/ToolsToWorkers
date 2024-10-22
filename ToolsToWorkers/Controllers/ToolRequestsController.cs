using Microsoft.AspNetCore.Mvc;
using ToolsToWorkers.Data.enums;
using ToolsToWorkers.Data.RepositoryInterfaces;
using ToolsToWorkers.Data.SearchData;
using ToolsToWorkers.Models;
using ToolsToWorkers.Models.Views;

namespace ToolsToWorkers.Controllers
{
    public class ToolRequestsController : Controller
    {
        private readonly IToolRequestRepository repository;
        private readonly IToolRepository toolRepository;

        public ToolRequestsController(IToolRequestRepository repository, IToolRepository toolRepository)
        {
            this.repository = repository;
            this.toolRepository = toolRepository;
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
                if (toolRequest.Status == ToolRequestStatus.Принят.ToString())
                {
                    Tool tool = await toolRepository.GetToolByIDAsyncNoTracking(toolRequest.ToolID);
                    tool.Status = ToolStatus.Доступен.ToString();
                    toolRepository.Update(tool);
                    toolRequest.Returned = DateTime.Now;
                }
                else
                {
                    Tool tool = await toolRepository.GetToolByIDAsyncNoTracking(toolRequest.ToolID);
                    tool.Status = ToolStatus.Занят.ToString();
                    toolRepository.Update(tool);
                    toolRequest.Returned = toolRequest.Returned.AddYears(-1);
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

        public async Task<IActionResult> Index(int page = 1, string searchDataStr = "")
        {
            ToolRequestSearchData searchData = ToolRequestSearchData.GetSearchData(searchDataStr);
            var toolRequests = FilterByStatus(await repository.GetAllDBSet(), searchData.Status);
            searchData.ToolRequests = await repository.GetSlice(searchData.PageNumber, searchData.PageSize, await GetToolRequests(searchData, toolRequests));
            searchData.elementsCount = GetToolRequests(searchData, toolRequests).Result.Count();
            return View(searchData);
        }
        [HttpPost]
        public async Task<IActionResult> Index(ToolRequestSearchData searchData)
        {
            var toolRequests = FilterByStatus(await repository.GetAllDBSet(), searchData.Status);
            searchData.ToolRequests = await repository.GetSlice(searchData.PageNumber, searchData.PageSize, await GetToolRequests(searchData, toolRequests));
            searchData.elementsCount = GetToolRequests(searchData, toolRequests).Result.Count();
            return View(searchData);
        }

        async Task<IQueryable<ToolRequestsView>> GetToolRequests(ToolRequestSearchData searchData, IQueryable<ToolRequestsView> filtered)
        {
            switch (searchData.Field)
            {
                case "Название":
                    return await repository.SearchByNameQuery(searchData.Value, filtered);
                case "Логин":
                    return await repository.SearchByLoginQuery(searchData.Value, filtered);
            }
            return await repository.GetAllDBSet();
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
