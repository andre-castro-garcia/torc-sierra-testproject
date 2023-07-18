using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Sierra.TakeHome.API.Controllers;
using Sierra.TakeHome.API.Models;
using Sierra.TakeHome.API.Requests;
using Sierra.TakeHome.Data.Entities;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace Sierra.TakeHome.UnitTests.Controllers; 

[TestFixture]
public class OrdersControllerTests {

    /// <summary>
    /// 
    /// </summary>
    private OrdersController _controller = null!;

    /// <summary>
    /// 
    /// </summary>
    private IMediator _mediator = null!;

    /// <summary>
    /// 
    /// </summary>
    [SetUp]
    public void Setup() {
        _mediator = Substitute.For<IMediator>();
        _controller = new OrdersController(_mediator);
    }

    /// <summary>
    /// 
    /// </summary>
    [Test]
    public async Task ShouldReturnBadRequestWhenModelInvalid() {
        var productId = Guid.NewGuid();
        const int quantity = 2;
        var requestModel = new CreateOrderModel {
            ProductId = productId,
            Quantity = quantity
        };
        _controller.ModelState.AddModelError("productId", "Invalid");
        var response = await _controller.CreateOrder(requestModel);
        
        Assert.That(((BadRequestResult)response).StatusCode, Is.EqualTo(400));
    }
    
    /// <summary>
    /// 
    /// </summary>
    [Test]
    public async Task ShouldReturnBadRequestWhenCreateOrderHandlerFailed() {
        var productId = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        const int quantity = 2;
        var requestModel = new CreateOrderModel {
            ProductId = productId,
            Quantity = quantity
        };
        var claims = new ClaimsPrincipal(new ClaimsIdentity(new [] {
            new Claim(JwtRegisteredClaimNames.Sid, customerId.ToString()),
        },"TestAuthentication"));
        _controller.ControllerContext = new ControllerContext {
            HttpContext = new DefaultHttpContext { User = claims }
        };

        _mediator.Send(Arg.Any<CreateOrderRequest>()).ThrowsAsync(new ArgumentException());
        
        var response = await _controller.CreateOrder(requestModel);
        Assert.That(((BadRequestResult)response).StatusCode, Is.EqualTo(400));
    }
    
    /// <summary>
    /// 
    /// </summary
    [Test]
    public async Task ShouldReturnBadRequestWhenProductNotFound() {
        var productId = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        const int quantity = 2;
        var requestModel = new CreateOrderModel {
            ProductId = productId,
            Quantity = quantity
        };
        var claims = new ClaimsPrincipal(new ClaimsIdentity(new [] {
            new Claim(JwtRegisteredClaimNames.Sid, customerId.ToString()),
        },"TestAuthentication"));
        _controller.ControllerContext = new ControllerContext {
            HttpContext = new DefaultHttpContext { User = claims }
        };

        _mediator.Send(Arg.Any<CreateOrderRequest>()).Returns(new OrderModel {
            CustomerId = customerId, Id = Guid.NewGuid(), ProductId = productId, Quantity = quantity, Total = 3
        });
        
        var response = await _controller.CreateOrder(requestModel);
        Assert.That(((OkObjectResult)response).StatusCode, Is.EqualTo(200));
    }
}