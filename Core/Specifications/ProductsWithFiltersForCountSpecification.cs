using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductsWithFiltersForCountSpecification(ProductSpecParams ProductParams) :
         base(x =>
         (string.IsNullOrEmpty(ProductParams.Search)||x.Name.
        ToLower().Contains(ProductParams.Search))&&
        (!ProductParams.brandId.HasValue||x.ProductBrandId==ProductParams.brandId)&&
        (!ProductParams.typeId.HasValue||x.ProductTypeId==ProductParams.typeId)
        )
        {
        }
    }
}