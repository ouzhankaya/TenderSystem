using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tender.Sourcing.Entities;
using Tender.Sourcing.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tender.Sourcing.Controllers
{
  [Route("api/v1/[controller]")]
  [ApiController]
  public class BidController : ControllerBase
  {
    private readonly IBidRepository _bidRepository;

    public BidController(IBidRepository bidRepository)
    {
      _bidRepository = bidRepository;
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult> SendBid([FromBody] Bid bid)
    {
      await _bidRepository.SendBid(bid);

      return Ok();
    }

    [HttpGet("GetBidsByTenderId")]
    [ProducesResponseType(typeof(List<Bid>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Bid>>> GetAllBidsByTenderId(string id)
    {
      IEnumerable<Bid> bids = await _bidRepository.GetBidsByTenderId(id);

      return Ok(bids);
    }

    [HttpGet("GetWinnerBid")]
    [ProducesResponseType(typeof(Bid), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Bid>> GetWinnerBid(string id)
    {
      Bid bid = await _bidRepository.GetWinnerBid(id);

      return Ok(bid);
    }
  }
}
