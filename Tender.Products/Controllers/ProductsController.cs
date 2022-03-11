using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tender.Products.Entities;
using Tender.Products.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tender.Products.Controllers
{
  [Route("api/v1/[controller]")]
  [ApiController]
  public class ProductsController : ControllerBase
  {
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductRepository productRepository, ILogger<ProductsController> logger)
    {
      _logger = logger;
      _productRepository = productRepository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
      var products = await _productRepository.GetProducts();
      return Ok(products);
    }

    [HttpGet("{id:length(24)}", Name = "GetProductById")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<Product>> GetProductById(string id)
    {
      var result = await _productRepository.GetProductById(id);

      if (result == null) {
        _logger.LogError($"Product not found with id: {id}");
        return NotFound();
      }

      return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<Product>> Create([FromBody] Product product)
    {
      await _productRepository.Create(product);
      return CreatedAtRoute("GetProductById", new { id = product.Id }, product);
    }

    [HttpPut]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> Update([FromBody] Product product)
    {
      return Ok(await _productRepository.Update(product));
    }

    [HttpDelete("{id:length(24)}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> Delete(string id)
    {
      var result = await _productRepository.GetProductById(id);
      if (result == null)
      {
        _logger.LogError($"Product not found with id: {id}");
        return NotFound();
      }

      await _productRepository.Delete(id);
      return NoContent();
    }
  }
}
