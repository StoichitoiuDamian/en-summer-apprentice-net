
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections;
using TMS.ApiEndava.Controllers;
using TMS.ApiEndava.Models;
using TMS.ApiEndava.Models.Dto;
using TMS.ApiEndava.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTeste
{
    [TestClass]
    public class EventControllerTests
    {
        Mock<IEventRepository> _eventRepositoryMock;
        Mock<IMapper> _mapperMoq;
        List<Event> _moqList;
        List<EventDto> _dtoMoq;
        Mock<ILogger<EventController>> _logger;


        [TestInitialize]
        public void SetupMoqData()
        {
            _eventRepositoryMock = new Mock<IEventRepository>();
            _mapperMoq = new Mock<IMapper>();
            _dtoMoq = new List<EventDto>();
            _logger = new Mock<ILogger<EventController>>();
            _moqList = new List<Event>
            {
                new Event
                { EventId = 1,
                  EventName = "EventName",
                  DescriptionEvent = "EventDescription",
                  EndDate = DateTime.Now,
                  StartDate = DateTime.Now,
                  EventType = new EventType{EventTypeId=1,EventTypeName="test event type"},
                  EventTypeId = 1,
                  Venue = new Venue{VenueId=1,VenueCapacity=12,VenueLocation="Mock location", VenueType="mock type"},
                  VenueId=1,
                }
            };
            _dtoMoq = new List<EventDto>
            {
                new EventDto
                {
                    EndDate = DateTime.Now,
                    EventDescription = "Moq description",
                    EventId = 1,
                    EventName = "Moq name",
                    StartDate = DateTime.Now,
                }
            };
        }

        [TestMethod]
        public async Task GetAllEventsReturnListOfEvents()
        {
            _eventRepositoryMock.Setup(moq => moq.GetAll()).Returns(_moqList);
            _mapperMoq.Setup(moq => moq.Map<IEnumerable<EventDto>>(It.IsAny<IReadOnlyList<Event>>())).Returns(_dtoMoq);
            var controller = new EventController(_eventRepositoryMock.Object, _mapperMoq.Object, _logger.Object);

            // Act
            var result = controller.GetAll();

            // Assert
            Assert.IsNotNull(result); // Ensure the result is not null
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            // Cast to OkObjectResult only if it's not null
            if (result.Result is OkObjectResult eventResult)
            {
                Assert.IsNotNull(eventResult.Value); // Ensure the value is not null
                Assert.IsInstanceOfType(eventResult.Value, typeof(IEnumerable<EventDto>));
                var eventCount = (eventResult.Value as IEnumerable<EventDto>).Count();
                Assert.AreEqual(_moqList.Count, eventCount);
            }
            else
            {
                Assert.Fail("Result is not of type OkObjectResult.");
            }
        }

        [TestMethod]
        public async Task DeleteEventReturnsNoContentResult()
        {
            // Arrange
            // Creăm un mock pentru IEventRepository și setăm comportamentul pentru GetById
            var eventRepositoryMock = new Mock<IEventRepository>();
            eventRepositoryMock.Setup(moq => moq.GetById(It.IsAny<long>())).ReturnsAsync(new Event());

            var eventController = new EventController(eventRepositoryMock.Object, null, null);

            // Act
            var result = await eventController.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult)); // Verificăm că rezultatul este de tip NoContentResult
            var noContentResult = (NoContentResult)result;
            Assert.AreEqual(204, noContentResult.StatusCode); // Verificăm că status code este 204 (NoContent)
        }
        [TestMethod]
        public async Task GetEventByIDThrowsAnException()
        {
            // Arrange
            _eventRepositoryMock.Setup(moq => moq.GetById(It.IsAny<int>())).Throws<Exception>();
            _mapperMoq.Setup(moq => moq.Map<EventDto>(It.IsAny<Event>())).Returns(_dtoMoq.First());
            var controller = new EventController(_eventRepositoryMock.Object, _mapperMoq.Object, _logger.Object);

            try
            {
                // Act
                var result = await controller.GetById(1);

                // Assert
                Assert.Fail("Expected an exception to be thrown.");
            }
            catch (Exception ex)
            {
                // Verificați aici dacă excepția este cea așteptată sau faceți alte verificări necesare.
                // În cazul de față, puteți verifica tipul excepției:
                Assert.IsInstanceOfType(ex, typeof(Exception));
            }
        }
        [TestMethod]
        public async Task GetEventByIdReturnFirstRecord()
        {
            //Arrange
            _eventRepositoryMock.Setup(moq => moq.GetById(It.IsAny<int>())).Returns(Task.Run(() => _moqList.First()));
            _mapperMoq.Setup(moq => moq.Map<EventDto>(It.IsAny<Event>())).Returns(_dtoMoq.First());
            var controller = new EventController(_eventRepositoryMock.Object, _mapperMoq.Object, _logger.Object);
            //Act

            var result = await controller.GetById(1);
            var eventResult = result.Result as OkObjectResult;
            var eventCount = eventResult.Value as EventDto;

            //Assert

            Assert.IsFalse(string.IsNullOrEmpty(eventCount.EventName));
            Assert.AreEqual(1, eventCount.EventId);
        }

        [TestMethod]
        public async Task Patch_ValidEvent_ReturnsOkObjectResult()
        {
            // Arrange
            var eventId = 1;
            var eventPatchDto = new EventPatchDto { EventId = eventId, EventName = "Updated Event Name" };
            var eventEntity = new Event { EventId = eventId, EventName = "Original Event Name" };

            _eventRepositoryMock.Setup(repo => repo.GetById(eventId)).ReturnsAsync(eventEntity);
            _mapperMoq.Setup(moq => moq.Map<EventPatchDto, Event>(eventPatchDto, eventEntity)).Callback(() =>
            {
                // Update eventEntity with the properties from eventPatchDto
                eventEntity.EventName = eventPatchDto.EventName;
            });

            var controller = new EventController(_eventRepositoryMock.Object, _mapperMoq.Object, _logger.Object);

            // Act
            var result = await controller.Patch(eventPatchDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOfType(okResult.Value, typeof(Event));
            var updatedEvent = okResult.Value as Event;
            Assert.AreEqual(eventId, updatedEvent.EventId);
            Assert.AreEqual(eventPatchDto.EventName, updatedEvent.EventName);
        }


    }

}
