using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sierra.TakeHome.Data.Entities;

namespace Sierra.TakeHome.Data.Database;

/// <summary>
/// 
/// </summary>
public partial class DatabaseContext {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="customerId"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    public async Task<Order> AddNewOrder(Guid productId, Guid customerId, int quantity) {
        var productIdParam = new SqlParameter(nameof(productId), productId);
        var customerIdParam = new SqlParameter(nameof(customerId), customerId);
        var quantityParam = new SqlParameter(nameof(quantity), quantity);

        var parameters = new List<SqlParameter> {
            productIdParam, customerIdParam, quantityParam
        };
        
        return (await Orders.FromSqlRaw($"execute [dbo].[AddNewOrder] @productId, @customerId, @quantity", parameters.ToArray())
            .ToListAsync()).Single();
    }
}