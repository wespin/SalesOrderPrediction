using AutoMapper;
using WebApi.Models;
using WebApi.DTOs;

namespace WebApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<OrderCreateDTO, Order>();
        }
    }
}
