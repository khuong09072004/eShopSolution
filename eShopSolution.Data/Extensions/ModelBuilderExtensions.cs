using eShopSolution.Data.Entities;
using eShopSolution.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfig>().HasData(
                new AppConfig() { Key = "HomeTitle", Value = "This is home page of eShopSolution" },
                new AppConfig() { Key = "HomeKeyWord", Value = "This is keyword of eShopSolution" },
                new AppConfig() { Key = "HomeDescription", Value = "This is description of eShopSolution" }
                );
            modelBuilder.Entity<Language>().HasData(
                new Language() { Id = "vi-VN", Name = "Tiếng Việt", IsDefault = true },
                new Language() { Id = "en-US", Name = "English", IsDefault = false }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOrder = 1,
                    Status = Status.Active,

                },
                new Category()
                {
                    Id = 2,
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOrder = 2,
                    Status = Status.Active,
                }
                );
            modelBuilder.Entity<CategoryTranslation>().HasData(
                new CategoryTranslation()
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "Ao nam",
                    LanguageId = "vi-VN",
                    SeoAlias = "ao-nam",
                    SeoDescription = "san pham ao thoi trang nam",
                    SeoTitle = "san pham ao thoi trang nam"
                },
                new CategoryTranslation()
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "Men Shirt",
                    LanguageId = "en-US",
                    SeoAlias = "men-shirt",
                    SeoDescription = "The shirt product for men",
                    SeoTitle = "The shirt product for men"
                },
                 new CategoryTranslation()
                 {
                     Id = 3,
                     CategoryId = 2,
                     Name = "Ao nu",
                     LanguageId = "vi-VN",
                     SeoAlias = "ao-nu",
                     SeoDescription = "san pham ao thoi trang nu",
                     SeoTitle = "san pham ao thoi trang nu"
                 },
                    new CategoryTranslation()
                    {
                        Id = 4,
                        CategoryId = 2,
                        Name = "Women Shirt",
                        LanguageId = "en-US",
                        SeoAlias = "women-shirt",
                        SeoDescription = "The shirt product for women",
                        SeoTitle = "The shirt product for women"
                    }
                );
            modelBuilder.Entity<Product>().HasData(
               new Product()
               {
                   Id = 1,
                   DateCreated = new DateTime(2024, 11, 12),
                   OriginalPrice = 100000,
                   Price = 200000,
                   Stock = 0,
                   ViewCount = 0,
               }
               );
            modelBuilder.Entity<ProductTranslation>().HasData(

                new ProductTranslation()
                {
                    Id = 1,
                    ProductId = 1,
                    Name = "Ao nam",
                    LanguageId = "vi-VN",
                    SeoAlias = "ao-nam",
                    SeoDescription = "san pham ao thoi trang nam",
                    SeoTitle = "san pham ao thoi trang nam",
                    Details = "Mo ta san pham",
                    Description = ""
                },
                    new ProductTranslation()
                    {
                        Id = 2,
                        ProductId = 1,
                        Name = "Men Shirt",
                        LanguageId = "en-US",
                        SeoAlias = "men-shirt",
                        SeoDescription = "The shirt product for men",
                        SeoTitle = "The shirt product for men",
                        Details = "Description of product",
                        Description = ""
                    });
            modelBuilder.Entity<ProductInCategory>().HasData(
                new ProductInCategory() { ProductId = 1, CategoryId = 1 }
                );

        }
    }
}
