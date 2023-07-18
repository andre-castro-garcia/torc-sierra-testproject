using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sierra.TakeHome.API.Models;
using Sierra.TakeHome.API.Requests;

namespace Sierra.TakeHome.API.Controllers; 

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase {

    /// <summary>
    /// 
    /// </summary>
    private readonly IMediator _mediator;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    public ProductsController(IMediator mediator) {
        _mediator = mediator;
    }
    
    /// <summary>
    /// 
    /// </summary>
    [HttpGet("{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesErrorResponseType(typeof(void))]
    public async Task<IActionResult> GetProduct(Guid productId) {
        var product = await _mediator.Send(new GetProductRequest { ProductId = productId });
        if (product == null) {
            return NotFound(null);
        }

        return Ok(product);
    }
}