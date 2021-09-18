using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public async  Task<ActionResult<List<Product>>> Get()
        { 
            IReadOnlyList<Product> products = await  this._productRepository.GetProductsAsync();           
            return Ok(products);
        }

        

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            Product product = await this._productRepository.GetProductByIdAsync(id);
            return product;
        }

        // POST api/<ProductsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        // GET: api/Products/ProductBrands
       
        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetBrands()
        {
            IReadOnlyList<ProductBrand> productBrands = await this._productRepository.GetProductBrandsAsync();
            return Ok(productBrands);
        }
        // GET: api/Products/ProductTypes

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductBrand>>> GetTypes()
        {
            IReadOnlyList<ProductType> productTypes = await this._productRepository.GetProductTypesAsync();
            return Ok(productTypes);
        }
    }
}
