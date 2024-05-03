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
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger, ICategoryRepository categoryRepository)
        {
            _logger = logger;
            this.categoryRepository = categoryRepository;
        }

        [HttpGet(Name = "GetCategories")]
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await categoryRepository.GetAllAsync();
        }
        [HttpGet(Name = "GetCategoriesByTab")]
        public async Task<IEnumerable<Category>> GetCategoriesByTabId(int tabId = -1)
        {
            if (tabId < 0)
            {
                return await categoryRepository.GetAllAsync();
            }
            return (await categoryRepository.GetAllAsync()).Where(x => x.CategoryCategoryId == tabId);
        }

        [HttpGet(Name = "GetCategory")]
        public async Task<IActionResult> GetCategory(int id)
        {
            return Ok(await categoryRepository.GetByIdAsync(id));
        }

        [HttpGet(Name = "GetCategoryTabs")]
        public async Task<IActionResult> GetCategoryTabs()
        {
            return Ok(new List<CategoryTabResponse> { 
                new CategoryTabResponse { Id = 0, Name = "Тренировки" },
                new CategoryTabResponse { Id = 1, Name = "Спа" } });
        }
    }
}