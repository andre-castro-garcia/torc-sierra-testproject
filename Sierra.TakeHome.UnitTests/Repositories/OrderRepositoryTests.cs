using MockQueryable.NSubstitute;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NSubstitute.ReturnsExtensions;
using Sierra.TakeHome.Data.Database;
using Sierra.TakeHome.Data.Entities;
using Sierra.TakeHome.Data.Interfaces;
using Sierra.TakeHome.Data.Repositories;

namespace Sierra.TakeHome.UnitTests.Repositories;

/// <summary>
/// 
/// </summary>
[TestFixture]
public class OrderRepositoryTests {
    /// <summary>
    /// 
    /// </summary>
    private IOrderRepository _repository = null!;

    /// <summary>
    /// 
    /// </summary>
    private DatabaseContext _context = null!;

    /// <summary>
    /// 
    /// </summary>
    private readonly Guid _orderIdValid = Guid.NewGuid();

    /// <summary>
    /// ß
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp() {
        SetupDatabaseContext();
        _repository = new OrderRepository(_context);
    }

    /// <summary>
    /// 
    /// </summary>
    private void SetupDatabaseContext() {
        var data = new List<Order> {
            new() {
                Id = _orderIdValid, Quantity = 2, ProductId = Guid.NewGuid(), CustomerId = Guid.NewGuid(), Total = 1.0m
            }
        }.AsQueryable().BuildMockDbSet();

        _context = Substitute.For<DatabaseContext>();
        _context.Orders.Returns(data);
    }

    /// <summary>
    /// 
    /// </summary>
    [Test]
    public async Task ShouldReturnOrderWhenOrderIdValid() {
        var product = await _repository.GetOrder(_orderIdValid);
        Assert.Multiple(() => {
            Assert.That(product!.Id, Is.EqualTo(_orderIdValid));
            Assert.That(product.ProductId, Is.Not.Empty);
            Assert.That(product.CustomerId, Is.Not.Empty);
        });
    }

    /// <summary>
    /// 
    /// </summary>
    [Test]
    public async Task ShouldReturnNullWhenOrderIdNotValid() {
        var order = await _repository.GetOrder(Guid.NewGuid());
        Assert.That(order, Is.Null);
    }
}