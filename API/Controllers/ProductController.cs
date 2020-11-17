using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController:ControllerBase
    {
        private readonly IGenericRepository<Product> _productrepo;
        private readonly IGenericRepository<ProductBrand> _brandrepo;
        private readonly IGenericRepository<ProductType> _producttyperepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productrepo,
                                  IGenericRepository<ProductBrand> brandrepo,
                                  IGenericRepository<ProductType> producttyperepo,
                                  IMapper mapper)
        {
            _productrepo = productrepo;
            this._brandrepo = brandrepo;
            this._producttyperepo = producttyperepo;
            this._mapper = mapper;
        }

        [HttpGet]
       public async Task<ActionResult<IReadOnlyList<ProductToReturn>>> GetProducts()
       {
           var spec=new ProductsWithTypesAndBrandsSpecification();
          var products=await _productrepo.ListAsync(spec);
          return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturn>>(products));
       } 
       [HttpGet("{id}")]
       public async Task<ActionResult<ProductToReturn>> GetProduct(int id)
       {
           var spec=new ProductsWithTypesAndBrandsSpecification(id);
           var product= await _productrepo.GetEntityWithSpec(spec);
           return _mapper.Map<Product , ProductToReturn>(product);
       }
       [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
       {
           return Ok(await _brandrepo.ListAllAsync());
       } 
       [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
       {
          
          return Ok(await _producttyperepo.ListAllAsync());
       } 
    }
}