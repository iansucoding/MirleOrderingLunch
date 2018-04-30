using Microsoft.AspNetCore.Mvc;
using MirleOrdering.Api.ViewModels;
using MirleOrdering.Data.Models;
using MirleOrdering.Service.Interfaces;
using MirleOrdering.Service.ViewModels;
using System;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MirleOrdering.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;

        }
        // GET: api/product
        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(_productService.GetAll());
        }

        // GET api/product/5
        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult Get(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return new ObjectResult(product);
        }

        // POST api/product
        [HttpPost]
        public IActionResult Create([FromBody]ProductBaseModel product)
        {
            if (product == null)
            {
                return BadRequest("product is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _productService.Create(product);
            if (result.IsSuccess)
            {
                var data = new 
                {
                    ProductId = long.Parse(result.Message),
                    product.ProductName,
                    product.Description,
                    product.Price,
                    product.CategoryId
                };
                return CreatedAtRoute("GetProduct", new { id = data.ProductId }, data);
            }
            return BadRequest(result);
        }

        // PUT api/product/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]ProductViewModel product)
        {
            if (product == null)
            {
                return BadRequest("product is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _productService.Update(product);
            if (result.IsSuccess)
            {
                return CreatedAtRoute("GetProduct", new { id = product.ProductId }, product);
            }
            return BadRequest(result);
        }

        // PUT api/product/5/patch
        [HttpPut("{id}/patch")]
        public IActionResult Patch(int id, [FromBody]ProductViewModel product)
        {
            if (product == null)
            {
                return BadRequest("product is null");
            }
            var result = _productService.Patch(product.ProductId, product.ProductName, product.Price, product.Description);
            if (result.IsSuccess)
            {
                return CreatedAtRoute("GetUser", new { id = product.ProductId }, product);
            }
            return BadRequest(result);
        }

        // DELETE api/product/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_productService.IsProductExisted(id))
            {
                return NotFound();
            }
            return Json(_productService.Delete(id));
        }
    }
}
