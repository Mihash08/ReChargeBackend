using BackendReCharge.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BackendReCharge.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CategoryController : ControllerBase
    {
        List<Category> categories = new List<Category>()
            {
                new Category("Бокс") {Id = 1},
                new Category("Теннис") {Id = 2},
                new Category("Фитнесс Зал") {Id = 3},
                new Category("Бассейн") {Id = 4},
            };
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetCategories")]
        public IEnumerable<Category> GetCategories()
        {
            return categories;
        }

        [HttpGet(Name = "GetCategory")]
        public Category GetCategory(int id)
        {
            return categories.Where(x => x.Id == id).First();
        }
    }
}