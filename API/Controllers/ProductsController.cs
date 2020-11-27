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
using API.Helpers;

namespace API.Controllers
{
    
    [Route("api/[Controller]")]
    public class ProductsController:BaseApiController
    {
        private readonly IGenericRepository<Product> _productrepo;
        private readonly IGenericRepository<ProductBrand> _brandrepo;
        private readonly IGenericRepository<ProductType> _producttyperepo;
        private readonly IGenericRepository<ProductState> _productstaterepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productrepo,
                                  IGenericRepository<ProductBrand> brandrepo,
                                  IGenericRepository<ProductType> producttyperepo,
                                  IGenericRepository<ProductState> productstaterepo,
                                  IMapper mapper)
        {
            _productrepo = productrepo;
            this._brandrepo = brandrepo;
            this._producttyperepo = producttyperepo;
            this._productstaterepo = productstaterepo;
            this._mapper = mapper;
        }

        [HttpGet]
       public async Task<ActionResult<Pagination<ProductToReturn>>> GetProducts(
           [FromQuery] ProductSpecParams productParams)
       {
           var spec=new ProductsWithTypesAndBrandsSpecification(productParams);
           var countSpec=new ProductsWithFiltersForCountSpecification(productParams);
           var totalItems=await _productrepo.CountAsync(countSpec);
          var products=await _productrepo.ListAsync(spec);
          var data=_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturn>>(products);
          return Ok(new Pagination<ProductToReturn>(productParams.PageIndex,productParams.PageSize,
          totalItems,data));
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
       [HttpGet("states")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductStates()
       {
          
          return Ok(await _productstaterepo.ListAllAsync());
       } 
    }
}