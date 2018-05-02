using Microsoft.AspNetCore.Mvc;
using MirleOrdering.Api.Services;
using MirleOrdering.Api.ViewModels;
using MirleOrdering.Service.Interfaces;
using MirleOrdering.Service.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MirleOrdering.Api.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly AppService _appService;

        public CategoryController(ICategoryService categoryService, AppService appService)
        {
            _categoryService = categoryService;
            _appService = appService;
        }
        // GET: api/category
        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(_categoryService.GetAll());
        }

        // GET api/category/5
        [HttpGet("{id}", Name = "GetCategory")]
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

        // POST api/category
        [HttpPost]
        public IActionResult Create(CategoryCreationModel model)
        {
            if (model == null)
            {
                return BadRequest("category is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vm = new CategoryBaseModel
            {
                CategoryName = model.CategoryName,
                Description = model.Description,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                Url = model.Url
            };
            var result = _categoryService.Create(vm);
            if (result.IsSuccess)
            {
                // 
                var data = new
                {
                    categoryId = long.Parse(result.Message),
                    model.CategoryName,
                    model.Description,
                    model.PhoneNumber,
                    model.Address
                };
                // upload file
                if (model.File != null && model.File.Length > 0)
                {
                    var imageName = $"category_{data.categoryId}";
                    var imagePath = _appService.Upload(model.File, imageName).Result;
                    var isUpdateImageSuccess = _categoryService.UpdateImageById(data.categoryId, imagePath);
                }
                return CreatedAtRoute("GetCategory", new { id = data.categoryId }, data);
            }
            return BadRequest(result);
        }

        // PUT api/product/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]CategoryViewModel model)
        {
            if (model == null)
            {
                return BadRequest("category is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _categoryService.Update(model);
            if (result.IsSuccess)
            {
                var data = new
                {
                    model.CategoryId,
                    model.CategoryName,
                    model.Description,
                    model.PhoneNumber,
                    model.Address
                };
                return CreatedAtRoute("GetCategory", new { id = model.CategoryId }, data);
            }
            return BadRequest(result);
        }

        // DELETE api/product/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_categoryService.GetById(id) == null)
            {
                return NotFound();
            }
            return Json(_categoryService.Delete(id));
        }
    }
}
