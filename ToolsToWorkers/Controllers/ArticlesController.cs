using Microsoft.AspNetCore.Mvc;
using ToolsToWorkers.Data.RepositoryInterfaces;
using ToolsToWorkers.Data.SearchData;
using ToolsToWorkers.Models.Views;
using ToolsToWorkers.Models;

namespace ToolsToWorkers.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticlesRepository repository;

        public ArticlesController(IArticlesRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> Edit(string ID)
        {
            var article = await repository.GetByIDAsyncNoTracking(ID);
            return View(article);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Article article)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(article);
                }
                repository.Update(article);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

            }
            return View(article);
        }

        public async Task<IActionResult> Details(string id)
        {
            Article article = await repository.GetByIDAsync(id);
            return View(article);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Article article)
        {
            if (!ModelState.IsValid)
            {
                return View(article);
            }
            repository.Add(article);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Article> articles = await repository.GetAll();
            ArticleSearchData searchData = new ArticleSearchData();
            searchData.Articles = articles;
            return View(searchData);
        }
        [HttpPost]
        public async Task<IActionResult> Index(ArticleSearchData searchData)
        {
            switch (searchData.Field)
            {
                case "Название":
                    searchData.Articles = await repository.SearchByName(searchData.Value, await repository.GetAllDBSet());
                    break;
                case "Артикул":
                    searchData.Articles = await repository.SearchByID(searchData.Value, await repository.GetAllDBSet());
                    break;
            }
            return View(searchData);
        }

    }
}
