using Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
     public class StoreContextSeed
    {
        public static async Task  SeedAsync(StoreContext context,ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Products.Any())
                {
                    var cotenu = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(cotenu);
                    foreach (Product product in products)
                    {
                        context.Products.Add(product);
                    }
                    await context.SaveChangesAsync();
                }
                if (!context.ProductBrands.Any())
                {
                    var cotenu = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var productBrands = JsonSerializer.Deserialize<List<ProductBrand>>(cotenu);
                    foreach (ProductBrand productBrand in productBrands)
                    {
                        context.ProductBrands.Add(productBrand);
                    }
                    await context.SaveChangesAsync();
                }
                if (!context.ProductTypes.Any())
                {
                    var cotenu = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var productTypes = JsonSerializer.Deserialize<List<ProductType>>(cotenu);
                    foreach (ProductType productType in productTypes)
                    {
                        context.ProductTypes.Add(productType);
                    }
                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message); 
            }
            
        }
    }
}
