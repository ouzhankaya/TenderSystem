using System;
using MongoDB.Driver;
using Tender.Products.Data.Interfaces;
using Tender.Products.Entities;
using Tender.Products.Settings;

namespace Tender.Products.Data
{
  public class ProductContext:IProductContext
  {
    public ProductContext(IProductDatabaseSettings settings)
    {
      var client = new MongoClient(settings.ConnectionStrings);
      var database = client.GetDatabase(settings.DatabaseName);
      Products = database.GetCollection<Product>(settings.CollectionName);

      ProductContextSeed.SeedData(Products);
    }
    public IMongoCollection<Product> Products { get; }
  }
}
