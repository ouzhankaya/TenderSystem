using System;
using MongoDB.Driver;
using Tender.Sourcing.Data.Interfacess;
using Tender.Sourcing.Entities;
using Tender.Sourcing.Settings;

namespace Tender.Sourcing.Data.Concrete
{
  public class TenderContext: ITenderContext
  {
    public TenderContext(ITenderDatabaseSettings settings)
    {
      var client = new MongoClient(settings.ConnectionStrings);
      var database = client.GetDatabase(settings.DatabaseName);

      Tenders = database.GetCollection<Entities.Tender>(nameof(Entities.Tender));
      Bids = database.GetCollection<Entities.Bid>(nameof(Entities.Bid));

      Bids.Indexes.CreateOne(Builders<Bid>.IndexKeys.Ascending(x => x.TenderId));
      Tenders.Indexes.CreateOne(Builders<Entities.Tender>.IndexKeys.Ascending(x => x.CreatedAt));
      TenderContextSeed.SeedData(Tenders);
    }

    public IMongoCollection<Entities.Tender> Tenders { get; }
    public IMongoCollection<Bid> Bids { get; }

  }
}
