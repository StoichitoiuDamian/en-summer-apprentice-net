using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TMS.ApiEndava.Controllers;
using TMS.ApiEndava.Models;
using TMS.ApiEndava.Models.Dto;
using TMS.ApiEndava.Repositories;

namespace UnitTeste
{
    [TestClass]
    public class OrderControllerTests
    {
        private Mock<IOrderRepository> _orderRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<ITicketCategoryRespository> _ticketCategoryRepositoryMock;

        [TestInitialize]
        public void Setup()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _mapperMock = new Mock<IMapper>();
            _ticketCategoryRepositoryMock = new Mock<ITicketCategoryRespository>();
        }

        [TestMethod]
        public void GetAll_ReturnsListOfOrders()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { OrderId = 1, NumberOfTickets = 3, TotalPrice = 150 },
                new Order { OrderId = 2, NumberOfTickets = 2, TotalPrice = 100 }
            };

            _orderRepositoryMock.Setup(repo => repo.GetAll()).Returns(orders);
            _mapperMock.Setup(mapper => mapper.Map<List<OrderDto>>(orders)).Returns(new List<OrderDto>
            {
                new OrderDto { OrderId = 1, NumberOfTickets = 3, TotalPrice = 150 },
                new OrderDto { OrderId = 2, NumberOfTickets = 2, TotalPrice = 100 }
            });

            var controller = new OrderController(_orderRepositoryMock.Object, _mapperMock.Object, _ticketCategoryRepositoryMock.Object);

            // Act
            var result = controller.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOfType(okResult.Value, typeof(List<OrderDto>));
            var dtoOrders = okResult.Value as List<OrderDto>;
            Assert.AreEqual(2, dtoOrders.Count);
            Assert.AreEqual(1, dtoOrders[0].OrderId);
            Assert.AreEqual(2, dtoOrders[1].OrderId);
        }

        [TestMethod]
        public async Task GetById_ExistingOrderId_ReturnsOrderDto()
        {
            // Arrange
            var orderId = 1;
            var order = new Order { OrderId = orderId, NumberOfTickets = 2, TotalPrice = 100 };
            _orderRepositoryMock.Setup(repo => repo.GetById(orderId)).ReturnsAsync(order);
            _mapperMock.Setup(mapper => mapper.Map<OrderDto>(order)).Returns(new OrderDto { OrderId = orderId, NumberOfTickets = 2, TotalPrice = 100 });

            var controller = new OrderController(_orderRepositoryMock.Object, _mapperMock.Object, _ticketCategoryRepositoryMock.Object);

            // Act
            var result = await controller.GetById(orderId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOfType(okResult.Value, typeof(OrderDto));
            var orderDto = okResult.Value as OrderDto;
            Assert.AreEqual(orderId, orderDto.OrderId);
            Assert.AreEqual(2, orderDto.NumberOfTickets);
            Assert.AreEqual(100, orderDto.TotalPrice);
        }

        [TestMethod]
        public async Task GetById_NonExistentOrderId_ReturnsNotFoundResult()
        {
            // Arrange
            var nonExistentOrderId = 999;
            _orderRepositoryMock.Setup(repo => repo.GetById(nonExistentOrderId)).ReturnsAsync((Order)null);

            var controller = new OrderController(_orderRepositoryMock.Object, _mapperMock.Object, _ticketCategoryRepositoryMock.Object);

            // Act
            var result = await controller.GetById(nonExistentOrderId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task Patch_NonExistentOrderId_ReturnsNotFoundResult()
        {
            // Arrange
            var nonExistentOrderId = 999;
            var orderPatchDto = new OrderPatchDto { OrderId = nonExistentOrderId, NumberOfTickets = 3, ticketCategoryId = 1 };
            _orderRepositoryMock.Setup(repo => repo.GetById(nonExistentOrderId)).ReturnsAsync((Order)null);

            var controller = new OrderController(_orderRepositoryMock.Object, _mapperMock.Object, _ticketCategoryRepositoryMock.Object);

            // Act
            var result = await controller.Patch(orderPatchDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }
      

        [TestMethod]
        public async Task Delete_ExistingOrderId_ReturnsNoContentResult()
        {
            // Arrange
            var orderId = 1;
            var orderEntity = new Order { OrderId = orderId, NumberOfTickets = 2, TotalPrice = 100 };
            _orderRepositoryMock.Setup(repo => repo.GetById(orderId)).ReturnsAsync(orderEntity);

            var controller = new OrderController(_orderRepositoryMock.Object, _mapperMock.Object, _ticketCategoryRepositoryMock.Object);

            // Act
            var result = await controller.Delete(orderId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var noContentResult = result as NoContentResult;
            Assert.AreEqual(204, noContentResult.StatusCode);
        }

        [TestMethod]
        public async Task Delete_NonExistentOrderId_ReturnsNotFoundResult()
        {
            // Arrange
            var nonExistentOrderId = 999;
            _orderRepositoryMock.Setup(repo => repo.GetById(nonExistentOrderId)).ReturnsAsync((Order)null);

            var controller = new OrderController(_orderRepositoryMock.Object, _mapperMock.Object, _ticketCategoryRepositoryMock.Object);

            // Act
            var result = await controller.Delete(nonExistentOrderId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}