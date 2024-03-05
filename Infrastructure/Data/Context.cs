using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data
{
    public class Context:DbContext
    {
        public Context(DbContextOptions dbContextOptions):base(dbContextOptions)
        {

            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach(var EntityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properitiesEntityTypes = EntityType.ClrType.GetProperties().Where(e=>e.PropertyType==typeof(decimal));
                    foreach(var prop in properitiesEntityTypes)
                    {
                        modelBuilder.Entity(EntityType.Name).Property(prop.Name).HasConversion<double>();
                    }
                }
            }
            /*
            modelBuilder.Entity<ProductBrand>().HasData(
               new List<ProductBrand>()
               {
                   new ProductBrand{Id=1,Name="phon"},
                 
               });
            modelBuilder.Entity<Productype>().HasData(
              new List<Productype>()
              {
                   new Productype{Id=1,Name="phon"},

              });
            modelBuilder.Entity<Product>().HasData(
                new List<Product>()
                {
                   new Product{Id=1,Name="Iphon",ProductTypeId=1,ProductBrandId=1},
                   new Product{Id=2,Name="Samsong",ProductTypeId=1,ProductBrandId=1},
                   new Product{Id=3,Name="Oppo",ProductTypeId=1,ProductBrandId=1},
                });*/
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<Productype> ProductTypes { get; set; }
    }
}
