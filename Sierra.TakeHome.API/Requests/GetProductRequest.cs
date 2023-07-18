using MediatR;
using Sierra.TakeHome.API.Models;

namespace Sierra.TakeHome.API.Requests; 

/// <summary>
/// 
/// </summary>
public class GetProductRequest : IRequest<ProductModel?> {

    /// <summary>
    /// 
    /// </summary>
    public Guid ProductId { get; init; }
}