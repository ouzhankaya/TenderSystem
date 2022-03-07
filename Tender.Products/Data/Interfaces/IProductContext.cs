using System;
using MongoDB.Driver;
using Tender.Products.Entities;

namespace Tender.Products.Data.Interfaces
{
  public interface IProductContext
  {
    IMongoCollection<Product> Products { get; }
  }
}
