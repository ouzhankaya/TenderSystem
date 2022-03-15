using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tender.Sourcing.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tender.Sourcing.Controllers
{
  [Route("api/v1/[controller]")]
  [ApiController]
  public class TenderController : ControllerBase
  {
    private readonly ITenderRepository _repository;
    private readonly ILogger<TenderController> _logger;

    public TenderController(ITenderRepository repository, ILogger<TenderController> logger)
    {
      _repository = repository;
      _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Entities.Tender>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Entities.Tender>>> GetTenders()
    {
      var tenders = await _repository.GetAll();
      return Ok(tenders);
    }

    [HttpGet("{id:length(24)}", Name = "GetTenderById")]
    [ProducesResponseType(typeof(Entities.Tender), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<Entities.Tender>> GetTenderById(string id)
    {
      var tender = await _repository.GetById(id);

      if (tender == null) {
        _logger.LogError($"Tender with id: {id} has not been found in database");
        return NotFound();
      }
      return Ok(tender);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Entities.Tender),(int)HttpStatusCode.Created)]
    public async Task<ActionResult<Entities.Tender>> Create([FromBody] Entities.Tender tender)
    {
      await _repository.Create(tender);
      return CreatedAtRoute("GetTenderById", new { id = tender.Id }, tender);
    }

    [HttpPut]
    [ProducesResponseType(typeof(Entities.Tender), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Entities.Tender>> Update([FromBody] Entities.Tender tender)
    {
      return Ok(await _repository.Update(tender));
    }

    [HttpDelete("{id:length(24)}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<Entities.Tender>> Delete(string id)
    {
      return Ok(await _repository.Delete(id));
    }
  }
}
