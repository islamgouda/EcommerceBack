using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(Context context)
        {
            if(!context.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText("../Infrastructure/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
             await   context.ProductBrands.AddRangeAsync(brands);
               
            }
            if(!context.ProductTypes.Any())
            {
                var typeData = File.ReadAllText("../Infrastructure/SeedData/types.json");
                var types=JsonSerializer.Deserialize<List<Productype>>(typeData);
             await   context.ProductTypes.AddRangeAsync(types);
                
            }
            if (!context.Products.Any())
            {
                var ProductData = File.ReadAllText("../Infrastructure/SeedData/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);
              await  context.Products.AddRangeAsync(Products);
            }
            if (context.ChangeTracker.HasChanges())
            {
                await context.SaveChangesAsync();
            }
          
        }
    }
}
