using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Tender.Products.Data.Interfaces;
using Tender.Products.Entities;
using Tender.Products.Repositories.Interfaces;

namespace Tender.Products.Repositories
{
  public class ProductRepository: IProductRepository
  {
    private readonly IProductContext _context;
    public ProductRepository(IProductContext context)
    {
      _context = context;
    }

    public async Task Create(Product product)
    {
      await _context.Products.InsertOneAsync(product);
    }

    public async Task<bool> Delete(string id)
    {
      var filter = Builders<Product>.Filter.Eq(x => x.Id, id);
      DeleteResult deleteResult = await _context.Products.DeleteOneAsync(filter);

      return deleteResult.DeletedCount > 0 && deleteResult.IsAcknowledged; 
    }

    public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
    {
      var filter = Builders<Product>.Filter.Eq(x => x.Category, categoryName);

      return await _context.Products.Aggregate().Match(filter).ToListAsync();
    }

    public async Task<Product> GetProductById(string id)
    {
      var filter = Builders<Product>.Filter.Eq(x => x.Id, id);
      return await _context.Products.Aggregate().Match(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByName(string name)
    {
      var filter = Builders<Product>.Filter.Eq(x => x.Name, name);
      return await _context.Products.Aggregate().Match(filter).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
      return await _context.Products.Find(x => true).ToListAsync();
    }

    public async Task<bool> Update(Product product)
    {
      var updateResult = await _context.Products.ReplaceOneAsync(filter: l => l.Id == product.Id, replacement: product);
      return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }
  }
}
