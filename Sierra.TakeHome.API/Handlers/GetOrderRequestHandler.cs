using AutoMapper;
using MediatR;
using Sierra.TakeHome.API.Models;
using Sierra.TakeHome.API.Requests;
using Sierra.TakeHome.Data.Interfaces;

namespace Sierra.TakeHome.API.Handlers; 

public class GetOrderRequestHandler : IRequestHandler<GetOrderRequest, OrderModel?> {

    /// <summary>
    /// 
    /// </summary>
    private readonly IOrderRepository _orderRepository;

    /// <summary>
    /// 
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="orderRepository"></param>
    /// <param name="mapper"></param>
    public GetOrderRequestHandler(IOrderRepository orderRepository, IMapper mapper) {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<OrderModel?> Handle(GetOrderRequest request, CancellationToken cancellationToken) {
        var order = await _orderRepository.GetOrder(request.OrderId);
        return order == null ? null : _mapper.Map<OrderModel>(order);
    }
}