using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrand.Any())
                {
                    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    foreach (var item in brands)
                    {
                        context.ProductBrand.Add(item);
                    }
                    await context.SaveChangesAsync();

                }
                if (!context.ProductTypes.Any())
                {
                    var typeData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);
                    foreach (var item in types)
                    {
                        context.ProductTypes.Add(item);
                    }
                    await context.SaveChangesAsync();

                }
                if (!context.ProductState.Any())
                {
                    var StateData = File.ReadAllText("../Infrastructure/Data/SeedData/States.json");
                    var types = JsonSerializer.Deserialize<List<ProductState>>(StateData);
                    foreach (var item in types)
                    {
                        context.ProductState.Add(item);
                    }
                    await context.SaveChangesAsync();

                }
                if (!context.Products.Any())
                {
                    var ProductData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var types = JsonSerializer.Deserialize<List<Product>>(ProductData);
                    foreach (var item in types)
                    {
                        context.Products.Add(item);
                    }
                    await context.SaveChangesAsync();

                }
                 if (!context.DeliveryMethods.Any())
                {
                    var dmData = File.ReadAllText("../Infrastructure/Data/SeedData/delivery.json");
                    var delivery = JsonSerializer.Deserialize<List<DeliveryMethod>>(dmData);
                    foreach (var item in delivery)
                    {
                        context.DeliveryMethods.Add(item);
                    }
                    await context.SaveChangesAsync();

                }
                
                
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError
                (ex.Message);


            }
        }
    }
}