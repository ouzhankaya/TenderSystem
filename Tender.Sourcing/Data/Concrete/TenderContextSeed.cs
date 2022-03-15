using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Tender.Sourcing.Entities;

namespace Tender.Sourcing.Data.Concrete
{
  public class TenderContextSeed
  {
    public static void SeedData(IMongoCollection<Entities.Tender> mongoCollection)
    {
      bool exists = mongoCollection.Find(x => true).Any();

      if (!exists)
      {
        mongoCollection.InsertManyAsync(GetPreconfiguredTenders());
      }
    }

    private static IEnumerable<Entities.Tender> GetPreconfiguredTenders()
    {
      return new List<Entities.Tender>()
            {
                new Entities.Tender()
                {
                    Name = "Tender 1",
                    Description = "Tender Desc 1",
                    CreatedAt = DateTime.Now,
                    StartedAt = DateTime.Now,
                    FinishedAt = DateTime.Now.AddDays(10),
                    ProductId = "60093337093d7352d5467341",
                    IncludeSellers = new List<string>()
                    {
                        "seller1@test.com",
                        "seller2@test.com",
                        "seller3@test.com"
                    },
                    Quantity = 5,
                    Status = (int)Status.Active
                },
                new Entities.Tender()
                {
                    Name = "Tender 2",
                    Description = "Tender Desc 2",
                    CreatedAt = DateTime.Now,
                    StartedAt = DateTime.Now,
                    FinishedAt = DateTime.Now.AddDays(10),
                    ProductId = "60093337093d7352d5467341",
                    IncludeSellers = new List<string>()
                    {
                        "seller1@test.com",
                        "seller2@test.com",
                        "seller3@test.com"
                    },
                    Quantity = 5,
                    Status = (int)Status.Active
                },
                new Entities.Tender()
                {
                    Name = "Tender 3",
                    Description = "Tender Desc 3",
                    CreatedAt = DateTime.Now,
                    StartedAt = DateTime.Now,
                    FinishedAt = DateTime.Now.AddDays(10),
                    ProductId = "60093337093d7352d5467341",
                    IncludeSellers = new List<string>()
                    {
                        "seller1@test.com",
                        "seller2@test.com",
                        "seller3@test.com"
                    },
                    Quantity = 5,
                    Status = (int)Status.Active
                }
            };
    }
  }
}