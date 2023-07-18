using AutoMapper;
using MediatR;
using Sierra.TakeHome.API.Models;
using Sierra.TakeHome.API.Requests;
using Sierra.TakeHome.Data.Interfaces;

namespace Sierra.TakeHome.API.Handlers; 

public class CreateOrderRequestHandler : IRequestHandler<CreateOrderRequest, OrderModel> {

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
    private readonly IMediator _mediator;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="orderRepository"></param>
    /// <param name="mapper"></param>
    /// <param name="mediator"></param>
    public CreateOrderRequestHandler(IOrderRepository orderRepository, IMapper mapper, IMediator mediator) {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _mediator = mediator;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<OrderModel> Handle(CreateOrderRequest request, CancellationToken cancellationToken) {
        var product = await _mediator.Send(new GetProductRequest { ProductId = request.ProductId }, cancellationToken);
        if (product == null) {
            throw new ArgumentException($"Product not found. productId = {request.ProductId}");
        }
        var order = await _orderRepository.CreateOrder(request.ProductId, request.CustomerId, request.Quantity);
        return _mapper.Map<OrderModel>(order);
    }
}