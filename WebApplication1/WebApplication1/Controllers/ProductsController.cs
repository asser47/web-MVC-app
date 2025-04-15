using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Security.Claims;
using WebApplication1.Authorization;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        [Route("")]
        [Authorize]
        [CheckPermission(Permission.ReadProducts)]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            var records = _dbContext.Set<Product>().ToList();
            return Ok(records);
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var record = _dbContext.Set<Product>().Find(id);
            return record == null ? NotFound() : Ok(record);
        }
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<int>> CreateProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            product.Id = 0;
            _dbContext.Set<Product>().Add(product);
            await _dbContext.SaveChangesAsync();
            return Ok(product.Id);
        }
        [HttpPut]
        [Route("")]
        public async Task<ActionResult> UpdateProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingProduct = _dbContext.Set<Product>().Find(product.Id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            existingProduct.Name = product.Name; // AutoMapper (DTO) instead
            existingProduct.Sku = product.Sku;   // AutoMapper (DTO) instead
            _dbContext.Set<Product>().Update(existingProduct);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingProduct = _dbContext.Set<Product>().Find(id);
            if (existingProduct == null)
            {
                return NotFound();
            } 
                _dbContext.Set<Product>().Remove(existingProduct);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}   
