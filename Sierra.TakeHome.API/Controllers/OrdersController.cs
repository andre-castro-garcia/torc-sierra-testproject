using System.IdentityModel.Tokens.Jwt;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sierra.TakeHome.API.Models;
using Sierra.TakeHome.API.Requests;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Sierra.TakeHome.API.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase {
    /// <summary>
    /// 
    /// </summary>
    private readonly IMediator _mediator;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    public OrdersController(IMediator mediator) {
        _mediator = mediator;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [HttpGet("{orderId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesErrorResponseType(typeof(void))]
    public async Task<IActionResult> GetOrder(Guid orderId) {
        var order = await _mediator.Send(new GetOrderRequest { OrderId = orderId });
        if (order == null) {
            return NotFound(null);
        }

        return Ok(order);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesErrorResponseType(typeof(void))]
    [Authorize]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderModel request) {
        if (!ModelState.IsValid) {
            return BadRequest();
        }

        var customerId = Guid.Parse(User.FindFirst(JwtRegisteredClaimNames.Sid)!.Value);
        try {
            var order = await _mediator.Send(new CreateOrderRequest
                { ProductId = request.ProductId, CustomerId = customerId, Quantity = request.Quantity });
            return Ok(order);
        }
        catch (ArgumentException) {
            // TODO: Implement log registering product not found, any other error continue throw
            return BadRequest();
        }
    }
}