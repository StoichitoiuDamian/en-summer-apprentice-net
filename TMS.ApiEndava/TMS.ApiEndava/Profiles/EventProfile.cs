using AutoMapper;
using TMS.ApiEndava.Models;
using TMS.ApiEndava.Models.Dto;

namespace TMS.ApiEndava.Profiles

{
    public class EventProfile : Profile
    {
        public EventProfile() 
        {
            CreateMap<Event1, EventDto>().ReverseMap();

            CreateMap<Event1,EventPatchDto>().ReverseMap();
        }
    }
}
