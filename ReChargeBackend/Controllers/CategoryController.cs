using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BackendReCharge.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly ICategoryCategoryRepository categoryCategoryRepository;
        //TODO: добавь категории вместо мока
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger, ICategoryRepository categoryRepository, ICategoryCategoryRepository categoryCategoryRepository)
        {
            _logger = logger;
            this.categoryRepository = categoryRepository;
            this.categoryCategoryRepository = categoryCategoryRepository;
        }

        [HttpGet(Name = "GetCategories")]
        public IEnumerable<Category> GetCategories()
        {
            return categoryRepository.GetAll();
        }
        [HttpGet(Name = "GetCategoriesByTab")]
        public IEnumerable<Category> GetCategories(int tabId)
        {
            return categoryRepository.GetAll().Where(x => x.CategoryCategoryId == tabId);
        }

        [HttpGet(Name = "GetCategory")]
        public Category GetCategory(int id)
        {
            return categoryRepository.GetById(id);
        }

        [HttpGet(Name = "GetCategoryTabs")]
        public IEnumerable<CategoryCategory> GetCategoryTabs()
        {
            return categoryCategoryRepository.GetAll();
        }
    }
}