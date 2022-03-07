using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tender.Products.Entities;

namespace Tender.Products.Repositories.Interfaces
{
  public interface IProductRepository
  {
    Task<IEnumerable<Product>> GetProducts();
    Task<Product> GetProductById(string id);
    Task<IEnumerable<Product>> GetProductByName(string name);
    Task<IEnumerable<Product>> GetProductByCategory(string categoryName);

    Task Create(Product product);
    Task<bool> Update(Product product);
    Task<bool> Delete(string id);
  }
}
