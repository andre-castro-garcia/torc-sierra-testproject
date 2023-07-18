using AutoMapper;
using Sierra.TakeHome.API.Models;
using Sierra.TakeHome.Data.Entities;

namespace Sierra.TakeHome.API.Mappings;

/// <summary>
/// 
/// </summary>
public class MappingProfile : Profile {
    /// <summary>
    /// 
    /// </summary>
    public MappingProfile() {
        CreateMap<Product, ProductModel>();
        CreateMap<Order, OrderModel>();
    }
}