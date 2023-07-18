using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sierra.TakeHome.Data.Database;
using Sierra.TakeHome.Data.Entities;
using Sierra.TakeHome.Data.Interfaces;

namespace Sierra.TakeHome.Data.Repositories;

/// <summary>
/// 
/// </summary>
public class OrderRepository : IOrderRepository {
    /// <summary>
    /// 
    /// </summary>
    private readonly DatabaseContext _context;

    /// <summary>
    /// 
    /// </summary>
    public OrderRepository(DatabaseContext context) {
        _context = context;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public async Task<Order?> GetOrder(Guid orderId) {
        var product = await (
            from p in _context.Orders
            where p.Id == orderId
            select p
        ).FirstOrDefaultAsync();
        return product;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="customerId"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    public async Task<Order> CreateOrder(Guid productId, Guid customerId, int quantity) {
        return await _context.AddNewOrder(productId, customerId, quantity);
    }
}