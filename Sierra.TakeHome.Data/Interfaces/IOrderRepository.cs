using Sierra.TakeHome.Data.Entities;

namespace Sierra.TakeHome.Data.Interfaces; 

/// <summary>
/// 
/// </summary>
public interface IOrderRepository {
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public Task<Order?> GetOrder(Guid orderId);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="customerId"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    public Task<Order> CreateOrder(Guid productId, Guid customerId, int quantity);
}