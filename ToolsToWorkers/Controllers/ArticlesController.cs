using Microsoft.AspNetCore.Mvc;
using ToolsToWorkers.Data.RepositoryInterfaces;
using ToolsToWorkers.Data.SearchData;
using ToolsToWorkers.Models.Views;
using ToolsToWorkers.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ToolsToWorkers.Data;

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
                if (!ModelState.IsValid || article.Weight <= 0)
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
            if (!ModelState.IsValid || article.Weight <= 0 || repository.ArticleTaken(article.ID))
            {
                if (repository.ArticleTaken(article.ID))
                {
                    MessegaMarkers.InvalidArticle = true;
                }
                return View(article);
            }
            repository.Add(article);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index(int page = 1, string searchDataStr = "")
        {
            ArticleSearchData searchData = ArticleSearchData.GetSearchData(searchDataStr);
            searchData.Articles = await repository.GetSlice(searchData.PageNumber, searchData.PageSize, await GetArticles(searchData));
            searchData.elementsCount = GetArticles(searchData).Result.Count();
            return View(searchData);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ArticleSearchData searchData)
        {
            searchData.Articles = await repository.GetSlice(searchData.PageNumber, searchData.PageSize, await GetArticles(searchData));
            searchData.elementsCount = GetArticles(searchData).Result.Count();
            return View(searchData);
        }
        async Task<IQueryable<Article>> GetArticles(ArticleSearchData searchData)
        {
            switch (searchData.Field)
            {
                case "Название":
                    return await repository.SearchByNameQuery(searchData.Value);
                case "Артикул":
                    return await repository.SearchByIDQuery(searchData.Value);
            }
            return await repository.GetAllDBSet();
        }
    }
}
