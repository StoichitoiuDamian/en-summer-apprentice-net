using AutoMapper;
using TMS.ApiEndava.Models;
using TMS.ApiEndava.Models.Dto;

namespace TMS.ApiEndava.Profiles

{
    public class EventProfile : Profile
    {
        public EventProfile() 
        {
            CreateMap<Event, EventDto>().ReverseMap();

            CreateMap<Event,EventPatchDto>().ReverseMap();
        }
    }
}
