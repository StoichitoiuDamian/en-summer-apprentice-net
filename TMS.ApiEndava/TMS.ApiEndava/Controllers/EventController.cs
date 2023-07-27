﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Net.WebSockets;
using TMS.ApiEndava.Models;
using TMS.ApiEndava.Models.Dto;
using TMS.ApiEndava.Repositories;

namespace TMS.ApiEndava.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
       
        public EventController(IEventRepository eventRepository,IMapper mapper)
        {
             
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<EventDto>> GetAll()
        {
            var events = _eventRepository.GetAll();


            var dtoEvents = events.Select(e => new EventDto()
            {
                EventId = e.EventId,
                EventDescription = e.DescriptionEvent,
                EventName = e.EventName,
                EventType = e.EventType?.EventTypeName ?? string.Empty,
                Venue = e.Venue?.VenueLocation ?? string.Empty
            });
            // LINQ Method Query
            // var filterEvent = events.Where(x => x.EventName == "John Egbert Live").FirstOrDefault();

            return Ok(dtoEvents);
        }


        [HttpGet]
        public async Task<ActionResult<EventDto>> GetById(int id)
        {
            var @event = await _eventRepository.GetById(id);



            if (@event == null)
            {
                return NotFound();
            }



            var eventDto = _mapper.Map<EventDto>(@event);



            return Ok(eventDto);
        }

        [HttpPatch]
        public async Task<ActionResult<EventPatchDto>> Patch(EventPatchDto eventPatch)
        {
           var eventEntity = await  _eventRepository.GetById(eventPatch.EventId);
            if(eventEntity == null)
            {
                return NotFound();
            }
            _mapper.Map(eventPatch, eventEntity);
            _eventRepository.Update(eventEntity);
            return Ok(eventEntity);

        }

        [HttpDelete] public  async Task<ActionResult> Delete(int id)
        {
            var eventEntity = await _eventRepository.GetById(id);
            if (eventEntity == null)
            {
                return NotFound();
            }
            _eventRepository.Delete(eventEntity);
            return NoContent();
        }
    }
}

