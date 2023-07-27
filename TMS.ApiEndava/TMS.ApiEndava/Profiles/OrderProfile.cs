using TMS.ApiEndava.Models.Dto;
using TMS.ApiEndava.Models;
using AutoMapper;

namespace TMS.ApiEndava.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order1, OrderDto>();
            CreateMap<OrderPatchDto, Order1>();
        }
    }
}
