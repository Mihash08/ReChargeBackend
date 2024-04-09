using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using ReChargeBackend.Responses;

namespace BackendReCharge.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        //TODO: добавь категории вместо мока
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger, ICategoryRepository categoryRepository)
        {
            _logger = logger;
            this.categoryRepository = categoryRepository;
        }

        [HttpGet(Name = "GetCategories")]
        public IEnumerable<Category> GetCategories()
        {
            return categoryRepository.GetAll();
        }
        [HttpGet(Name = "GetCategoriesByTab")]
        public IEnumerable<Category> GetCategoriesByTabId(int tabId)
        {
            return categoryRepository.GetAll().Where(x => x.CategoryCategoryId == tabId);
        }

        [HttpGet(Name = "GetCategory")]
        public Category GetCategory(int id)
        {
            return categoryRepository.GetById(id);
        }

        [HttpGet(Name = "GetCategoryTabs")]
        public IEnumerable<CategoryTabResponse> GetCategoryTabs()
        {
            return new List<CategoryTabResponse> { new CategoryTabResponse { Id = 0, Name = "Тренировки" }, new CategoryTabResponse { Id = 1, Name = "Спа" } };
        }
    }
}