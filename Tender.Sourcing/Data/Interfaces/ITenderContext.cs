using System;
using MongoDB.Driver;
using Tender.Sourcing.Entities;

namespace Tender.Sourcing.Data.Interfacess
{
  public interface ITenderContext
  {
    IMongoCollection<Entities.Tender> Tenders { get; }
    IMongoCollection<Bid> Bids { get; }
  }
}
