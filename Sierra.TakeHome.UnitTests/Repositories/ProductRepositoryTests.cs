using MockQueryable.NSubstitute;
using NSubstitute;
using Sierra.TakeHome.Data.Database;
using Sierra.TakeHome.Data.Entities;
using Sierra.TakeHome.Data.Interfaces;
using Sierra.TakeHome.Data.Repositories;

namespace Sierra.TakeHome.UnitTests.Repositories;

/// <summary>
/// 
/// </summary>
[TestFixture]
public class ProductRepositoryTests {
    /// <summary>
    /// 
    /// </summary>
    private IProductRepository _repository = null!;

    /// <summary>
    /// 
    /// </summary>
    private DatabaseContext _context = null!;

    /// <summary>
    /// 
    /// </summary>
    private readonly Guid _productIdValid = Guid.NewGuid();

    /// <summary>
    /// 
    /// </summary>
    private const string ProductNameValid = "Product";

    /// <summary>
    /// ß
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp() {
        SetupDatabaseContext();
        _repository = new ProductRepository(_context);
    }

    /// <summary>
    /// 
    /// </summary>
    private void SetupDatabaseContext() {
        var data = new List<Product> {
            new() { Id = _productIdValid, Name = ProductNameValid, Price = 0.1m }
        }.AsQueryable().BuildMockDbSet();

        _context = Substitute.For<DatabaseContext>();
        _context.Products.Returns(data);
    }

    /// <summary>
    /// 
    /// </summary>
    [Test]
    public async Task ShouldReturnProductWhenProductIdValid() {
        var product = await _repository.GetProduct(_productIdValid);
        Assert.Multiple(() => {
            Assert.That(product!.Id, Is.EqualTo(_productIdValid));
            Assert.That(product.Name, Is.EqualTo(ProductNameValid));
        });
    }

    /// <summary>
    /// 
    /// </summary>
    [Test]
    public async Task ShouldReturnNullWhenProductIdNotValid() {
        var product = await _repository.GetProduct(Guid.NewGuid());
        Assert.That(product, Is.Null);
    }
}