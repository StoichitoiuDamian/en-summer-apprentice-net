using TMS.ApiEndava.Models.Dto;
using TMS.ApiEndava.Models;
using AutoMapper;

namespace TMS.ApiEndava.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderPatchDto, Order>();
        }
    }
}
