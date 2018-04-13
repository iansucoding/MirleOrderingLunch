using Microsoft.AspNetCore.Mvc;
using MirleOrdering.Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MirleOrdering.Api.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        // GET: api/category
        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(_categoryService.GetAll());
        }

        // GET api/category/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound();
            };
            return new ObjectResult(category);
        }

        // GET api/category/products
        [HttpGet("products", Name = "CategoriesWithProducts")]
        public IActionResult GetAllWithProducts()
        {
            return new ObjectResult(_categoryService.GetAllWithProducts());
        }

        // GET api/category/5/products
        [HttpGet("{id}/products", Name = "CategoryWithProducts")]
        public IActionResult GetWithProducts(int id)
        {
            var category = _categoryService.GetByIdWithProducts(id);
            if (category == null)
            {
                return NotFound();
            }
            return new ObjectResult(category);
        }



    }
}
