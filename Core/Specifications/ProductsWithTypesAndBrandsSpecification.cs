using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams ProductParams)
        :base(x =>
        (string.IsNullOrEmpty(ProductParams.Search)||x.Name.
        ToLower().Contains(ProductParams.Search))&&
        (!ProductParams.brandId.HasValue||x.ProductBrandId==ProductParams.brandId)&&
        (!ProductParams.typeId.HasValue||x.ProductTypeId==ProductParams.typeId)
        )
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            AddOrderBy(p=>p.Name);
            ApplyPaging(ProductParams.PageSize*(ProductParams.PageIndex-1),ProductParams.PageSize);
            if(!string.IsNullOrEmpty(ProductParams.Sort))
            {
                switch(ProductParams.Sort)
                {
                    case "priceAsc":
                            AddOrderBy(p=>p.Price);
                            break;
                    case "priceDesc":
                            AddOrderByDescending(p=>p.Price);
                            break;
                    default:
                            AddOrderBy(p=>p.Name);
                            break;

                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) :
        base(x => x.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}