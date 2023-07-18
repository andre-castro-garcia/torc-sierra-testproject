using Microsoft.EntityFrameworkCore;
using Sierra.TakeHome.Data.Database;
using Sierra.TakeHome.Data.Entities;
using Sierra.TakeHome.Data.Interfaces;

namespace Sierra.TakeHome.Data.Repositories;

/// <summary>
/// 
/// </summary>
public class ProductRepository : IProductRepository {
    /// <summary>
    /// 
    /// </summary>
    private readonly DatabaseContext _context;

    /// <summary>
    /// 
    /// </summary>
    public ProductRepository(DatabaseContext context) {
        _context = context;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    public async Task<Product?> GetProduct(Guid productId) {
        var product = await (
            from p in _context.Products
            where p.Id == productId
            select p
        ).FirstOrDefaultAsync();
        return product;
    }
}