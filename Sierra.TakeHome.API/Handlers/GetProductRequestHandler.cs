using AutoMapper;
using MediatR;
using Sierra.TakeHome.API.Models;
using Sierra.TakeHome.API.Requests;
using Sierra.TakeHome.Data.Interfaces;

namespace Sierra.TakeHome.API.Handlers; 

public class GetProductRequestHandler : IRequestHandler<GetProductRequest, ProductModel?> {

    /// <summary>
    /// 
    /// </summary>
    private readonly IProductRepository _productRepository;

    /// <summary>
    /// 
    /// </summary>
    private readonly IMapper _mapper;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="productRepository"></param>
    /// <param name="mapper"></param>
    public GetProductRequestHandler(IProductRepository productRepository, IMapper mapper) {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ProductModel?> Handle(GetProductRequest request, CancellationToken cancellationToken) {
        var product = await _productRepository.GetProduct(request.ProductId);
        return product == null ? null : _mapper.Map<ProductModel>(product);
    }
}