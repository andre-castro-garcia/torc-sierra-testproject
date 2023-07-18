using MediatR;
using Sierra.TakeHome.API.Models;

namespace Sierra.TakeHome.API.Requests; 

/// <summary>
/// 
/// </summary>
public class GetOrderRequest : IRequest<OrderModel?> {
    
    /// <summary>
    /// 
    /// </summary>
    public Guid OrderId { get; init; }
}