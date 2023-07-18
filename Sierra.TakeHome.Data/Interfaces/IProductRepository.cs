using Sierra.TakeHome.Data.Entities;

namespace Sierra.TakeHome.Data.Interfaces; 

/// <summary>
/// 
/// </summary>
public interface IProductRepository {
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    public Task<Product?> GetProduct(Guid productId);
}