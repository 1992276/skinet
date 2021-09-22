using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{    
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo,IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> productTypeRepo,IMapper mapper)
        {
            this._productRepo = productRepo;
            this._productBrandRepo = productBrandRepo;
            this._productTypeRepo = productTypeRepo;
            this._mapper = mapper;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public async  Task<ActionResult<List<ProductToReturnDto>>> Get()
        {
            var spec = new ProductWithTypesAndBrandSpecification();
            IReadOnlyList<Product> products = await  this._productRepo.ListAsync(spec);

           // List<ProductToReturnDto> productDto = products.Select(p => this._mapper.Map<Product, ProductToReturnDto>(p)).ToList();

            return Ok(this._mapper.Map<List<Product>,List<ProductToReturnDto>>(products.ToList()));
        }

        

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> Get(int id)
        {
           
            ISpecification<Product> spec = new ProductWithTypesAndBrandSpecification(p=>p.Id == id);
            Product product = await this._productRepo.GetEntityWithSpec(spec);
            if (product is null)
            {
                return NotFound(new ApiResponse(404));
            }
           
            return this._mapper.Map<Product,ProductToReturnDto>(product);


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


        // GET: api/Products/Brands
       
        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetBrands()
        {
            IReadOnlyList<ProductBrand> productBrands = await this._productBrandRepo.ListAllAsync();
            return Ok(productBrands);
        }
        // GET: api/Products/Types

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetTypes()
        {
            IReadOnlyList<ProductType> productTypes = await this._productTypeRepo.ListAllAsync() ;
            return Ok(productTypes);
        }
    }
}
