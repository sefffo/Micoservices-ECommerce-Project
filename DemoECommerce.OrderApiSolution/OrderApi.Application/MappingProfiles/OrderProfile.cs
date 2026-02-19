using AutoMapper;
using OrderApi.Application.DTOS;
using OrderApi.Domain.Entities;

namespace OrderApi.Application.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {

            CreateMap<Order, OrderDTO>().ReverseMap();

            CreateMap<Order, OrderDetailsDTO>()
             .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
            // These come from Product API (ProductDTO)
            .ForMember(dest => dest.ProductName, opt => opt.Ignore())
            .ForMember(dest => dest.UnitPrice, opt => opt.Ignore())
            .ForMember(dest => dest.TotalPrice, opt => opt.Ignore())
            // These come from Auth API (AppUserDTO)
            .ForMember(dest => dest.Email, opt => opt.Ignore())
            .ForMember(dest => dest.TelephoneNumber, opt => opt.Ignore());

        }
    }
}
