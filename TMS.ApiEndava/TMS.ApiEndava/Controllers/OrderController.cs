using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMS.ApiEndava.Models;
using TMS.ApiEndava.Models.Dto;
using TMS.ApiEndava.Repositories;

namespace TMS.ApiEndava.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ITicketCategoryRespository _ticketCategoryRespository;

        public OrderController(IOrderRepository orderRepository, IMapper mapper, ITicketCategoryRespository ticketCategoryRespository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _ticketCategoryRespository = ticketCategoryRespository;
          
        }

        [HttpGet]
        public ActionResult<List<OrderDto>> GetAll()
        {
            var orders = _orderRepository.GetAll();

            var dtoOrders = _mapper.Map<List<OrderDto>>(orders);

            return Ok(dtoOrders);
        }

        [HttpGet]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            var @order = await _orderRepository.GetById(id);



            if (@order == null)
            {
                return NotFound();
            }



            var orderDto = _mapper.Map<OrderDto>(@order);



            return Ok(orderDto);
        }

        [HttpPatch]
        public async Task<ActionResult<OrderPatchDto>> Patch(OrderPatchDto orderPatch)
        {
            var orderEntity = await _orderRepository.GetById(orderPatch.OrderId);
            if (orderEntity == null)
            {
                return NotFound();
            }

            if (orderPatch.ticketCategoryId != 0)
            {
                var ticketCategory = await _ticketCategoryRespository.GetById((int)orderPatch.ticketCategoryId);
                   
                if (ticketCategory == null)
                {
                    return NotFound("Ticket category not found");
                }

                double totalPrice = (double)(ticketCategory.Price * orderPatch.NumberOfTickets);
                orderEntity.TotalPrice = totalPrice;
            }
            else
            {
                return NotFound("Ticket category ID is missing or invalid");
            }

            _mapper.Map(orderPatch, orderEntity);
            _orderRepository.Update(orderEntity);
            return Ok(orderEntity);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var orderEntity = await _orderRepository.GetById(id);
            if (orderEntity == null)
            {
                return NotFound();
            }
            _orderRepository.Delete(orderEntity);
            return NoContent();
        }
    }
}