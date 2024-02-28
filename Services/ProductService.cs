using System.Collections.Generic;
using App.Models;

namespace App.Services
{
    public class ProductService : List<ProductModel>
    {
        public ProductService()
        {
            this.AddRange(new ProductModel[] {
                new ProductModel() { Id = 1, Name ="Iphone 15",Price = 1000},
                new ProductModel() { Id = 2, Name ="Samsung galaxy",Price = 500},
                new ProductModel() { Id = 3, Name ="Sony xyz",Price = 800},
                new ProductModel() { Id = 4, Name ="Nokia ac",Price = 100},
            });
        }
    }
}