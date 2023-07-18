using AutoMapper;
using MediatR;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Sierra.TakeHome.API.Handlers;
using Sierra.TakeHome.API.Models;
using Sierra.TakeHome.API.Requests;
using Sierra.TakeHome.Data.Entities;
using Sierra.TakeHome.Data.Interfaces;

namespace Sierra.TakeHome.UnitTests.Handlers;

/// <summary>
/// 
/// </summary>
[TestFixture]
public class CreateOrderRequestHandlerTests {
    /// <summary>
    /// 
    /// </summary>
    private CreateOrderRequestHandler _handler = null!;

    /// <summary>
    /// 
    /// </summary>
    private IOrderRepository _repository = null!;

    /// <summary>
    /// 
    /// </summary>
    private IMapper _mapper = null!;

    /// <summary>
    /// 
    /// </summary>
    private IMediator _mediator = null!;

    /// <summary>
    /// 
    /// </summary>
    [SetUp]
    public void Setup() {
        _repository = Substitute.For<IOrderRepository>();
        _mapper = Substitute.For<IMapper>();
        _mediator = Substitute.For<IMediator>();

        _handler = new CreateOrderRequestHandler(_repository, _mapper, _mediator);
    }

    /// <summary>
    /// ß
    /// </summary>
    [Test]
    public void ShouldThrowArgumentExceptionWhenProductNotFound() {
        var createOrderRequest = new CreateOrderRequest {
            ProductId = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            Quantity = 2
        };
        _mediator.Send(Arg.Any<GetProductRequest>(), Arg.Any<CancellationToken>()).ReturnsNull();

        Assert.CatchAsync<ArgumentException>(async () => {
            await _handler.Handle(createOrderRequest, CancellationToken.None);
        });
    }

    /// <summary>
    /// 
    /// </summary>
    [Test]
    public async Task ShouldReturnNewOrderWhenProductFound() {
        var productId = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        const int quantity = 2;

        var createOrderRequest = new CreateOrderRequest {
            ProductId = productId,
            CustomerId = customerId,
            Quantity = quantity
        };
        var order = new Order
            { ProductId = productId, CustomerId = customerId, Quantity = quantity, Total = quantity * 0.50m };
        _mediator.Send(Arg.Any<GetProductRequest>(), Arg.Any<CancellationToken>())
            .Returns(new ProductModel { Id = productId, Name = "Product A", Price = 0.6m });
        _repository.CreateOrder(Arg.Any<Guid>(), Arg.Any<Guid>(), Arg.Any<int>())
            .Returns(order);
        _mapper.Map<OrderModel>(Arg.Any<Order>()).Returns(new OrderModel {
            CustomerId = customerId,
            ProductId = productId,
            Quantity = quantity,
            Id = Guid.NewGuid(),
            Total = quantity * 0.5m,
        });

        var result = await _handler.Handle(new CreateOrderRequest {
            CustomerId = customerId,
            ProductId = productId,
            Quantity = 2
        }, CancellationToken.None);

        Assert.Multiple(() => {
            Assert.That(productId, Is.EqualTo(result.ProductId));
            Assert.That(customerId, Is.EqualTo(result.CustomerId));
            Assert.That(result.Quantity, Is.EqualTo(quantity));
        });
    }
}