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

        [HttpGet(Name = "GetCategory")]
        public Category GetCategory(int id)
        {
            return categoryRepository.GetById(id);
        }
    }
}