using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tender.Sourcing.Entities;

namespace Tender.Sourcing.Repositories.Interfaces
{
  public interface IBidRepository
  {
    Task SendBid(Bid bid);
    Task<List<Bid>> GetBidsByTenderId(string tenderId);
    Task<Bid> GetWinnerBid(string tenderIds);
  }
}
