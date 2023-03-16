using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return Ok(
                _productService.GetAll()
            );
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Product>> Get(int id)
        {
            var product = _productService.Get(id);

            if (product.Productid == 0)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
