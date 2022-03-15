using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Tender.Sourcing.Data.Interfacess;
using Tender.Sourcing.Entities;
using Tender.Sourcing.Repositories.Interfaces;

namespace Tender.Sourcing.Repositories
{
  public class BidRepository: IBidRepository
  {
    private readonly ITenderContext _context;
    public BidRepository(ITenderContext context)
    {
      _context = context;
    }

    public async Task<List<Bid>> GetBidsByTenderId(string tenderId)
    {
      FilterDefinition<Bid> filter = Builders<Bid>.Filter.Eq(x => x.TenderId, tenderId);
      List<Bid> bids = await _context.Bids.Aggregate().Match(filter).ToListAsync();

      bids = bids.OrderByDescending(a => a.CreatedAt)
                     .GroupBy(a => a.SellerUserName)
                     .Select(a => new Bid
                     {
                       TenderId = a.FirstOrDefault().TenderId,
                       Price = a.FirstOrDefault().Price,
                       CreatedAt = a.FirstOrDefault().CreatedAt,
                       SellerUserName = a.FirstOrDefault().SellerUserName,
                       ProductId = a.FirstOrDefault().ProductId,
                       Id = a.FirstOrDefault().Id
                     })
                     .ToList();

      return bids;
    }

    public async Task<Bid> GetWinnerBid(string tenderId)
    {
      var bids = await GetBidsByTenderId(tenderId);
      return bids.OrderByDescending(x => x.Price).FirstOrDefault();
    }

    public async Task SendBid(Bid bid)
    {
      await _context.Bids.InsertOneAsync(bid);
    }
  }
}
