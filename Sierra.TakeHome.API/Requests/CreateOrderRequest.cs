using MediatR;
using Sierra.TakeHome.API.Models;

namespace Sierra.TakeHome.API.Requests; 

/// <summary>
/// 
/// </summary>
public class CreateOrderRequest : IRequest<OrderModel> {
    
    /// <summary>
    /// 
    /// </summary>
    public Guid ProductId { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public Guid CustomerId { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public int Quantity { get; init; }
}