using HCF.HPPA.API.Controllers;
using HCF.HPPA.Common.Models;
using HCF.HPPA.Domain.Commands;
using HCF.HPPA.Domain.Queries;
using HCF.HPPA.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace HCF.HPPA.UnitTest.API.Controllers
{
    public class ProgramBenefitScheduleControllerTests
    {
        private readonly Mock<ILogger<ProgramBenefitScheduleController>> _loggerMock;
        private readonly Mock<IProgramBenefitScheduleService> _serviceMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ProgramBenefitScheduleController _controller;

        public ProgramBenefitScheduleControllerTests()
        {
            _loggerMock = new Mock<ILogger<ProgramBenefitScheduleController>>();
            _serviceMock = new Mock<IProgramBenefitScheduleService>();
            _mediatorMock = new Mock<IMediator>();
            _controller = new ProgramBenefitScheduleController(_loggerMock.Object, _serviceMock.Object, _mediatorMock.Object);
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WithSchedules()
        {
            // Arrange
            var schedules = new List<ProgramBenefitSchedule>
        {
               new ProgramBenefitSchedule { Id = 1, ProgramCode = "NGJ", MBSItemCode = "49318", MBSScheduleFees = 49093, ProgramMedicalFees = 49094,DateOn= DateTime.Now,DateOff=DateTime.Today.AddMonths(1),ChangedBy= "dino",Comments= "Comments6",Status= "Publish"},
                  new ProgramBenefitSchedule { Id = 2, ProgramCode = "NGJ" , MBSItemCode = "00000",MBSScheduleFees =10000,ProgramMedicalFees =20000,DateOn= DateTime.Now,DateOff=DateTime.Today.AddMonths(1),ChangedBy= "xyz",Comments= "Comments2",Status= "Publish"}
        };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllProgramBenefitScheduleQuery>(), default))
                    .ReturnsAsync(schedules);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnSchedules = Assert.IsType<List<ProgramBenefitSchedule>>(okResult.Value);
            Assert.Equal(2, returnSchedules?.Count);
        }

        [Fact]
        public async Task GetById_InvalidId_ShouldReturnBadRequest()
        {
            // Arrange
            long id = 1;
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetByIdProgramBenefitScheduleQuery>(), default))
                         .ReturnsAsync((ProgramBenefitSchedule)null);

            // Act
            var result = await _controller.GetById(id);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task GetById_ValidId_ShouldReturnOkResult()
        {
            // Arrange
            long id = 1;

            var schedule = new ProgramBenefitSchedule { Id = 1, ProgramCode = "NGJ", MBSItemCode = "49318", MBSScheduleFees = 49093, ProgramMedicalFees = 49094, DateOn = DateTime.Now, DateOff = DateTime.Today.AddMonths(1), ChangedBy = "dino", Comments = "Comments6", Status = "Publish" };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetByIdProgramBenefitScheduleQuery>(), default))
                         .ReturnsAsync(schedule);

            // Act
            var result = await _controller.GetById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(schedule, okResult.Value);
        }
        [Fact]
        public async Task Post_ValidSchedule_ShouldReturnCreatedResult()
        {
            // Arrange

            var newSchedule = new CreateProgramBenefitScheduleCommand { ProgramCode = "NGJ", MBSItemCode = "49318", MBSScheduleFees = 49093, ProgramMedicalFees = 49094, DateOn = DateTime.Now, DateOff = DateTime.Today.AddMonths(1), ChangedBy = "dino", Comments = "Comments6", Status = "Publish" };


            var createdSchedule = new ProgramBenefitSchedule() { Id= 1, ProgramCode = "NGJ", MBSItemCode = "49318", MBSScheduleFees = 49093, ProgramMedicalFees = 49094, DateOn = DateTime.Now, DateOff = DateTime.Today.AddMonths(1), ChangedBy = "dino", Comments = "Comments6", Status = "Publish" };

            _mediatorMock.Setup(m => m.Send(newSchedule, default))
                         .ReturnsAsync(createdSchedule);

            // Act
            var result = await _controller.Post(newSchedule);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnSchedule = Assert.IsType<ProgramBenefitSchedule>(createdAtActionResult.Value);
            Assert.Equal(newSchedule.ProgramCode, returnSchedule.ProgramCode);
        }


        [Fact]
        public async Task Put_ValidSchedule_ShouldReturnOkResult()
        {
            // Arrange
            long id = 1;
            var updateSchedule = new UpdateProgramBenefitScheduleCommand { Id = id, ProgramCode = "NGJ", MBSItemCode = "49318", MBSScheduleFees = 49093, ProgramMedicalFees = 49094, DateOn = DateTime.Now, DateOff = DateTime.Today.AddMonths(1), ChangedBy = "dino", Comments = "Comments1", Status = "Publish" };

            _mediatorMock.Setup(m => m.Send(updateSchedule, default))
                         .ReturnsAsync(true);

            // Act
            var result = await _controller.Put(id, updateSchedule);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(true, okResult.Value);
        }

        [Fact]
        public async Task Delete_ValidId_ShouldReturnOkResult()
        {
            // Arrange
            long id = 1;
            var command = new DeleteProgramBenefitScheduleCommand { Id = id };
            _mediatorMock.Setup(m => m.Send(command, default))
                         .ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetProgramBenefitSchedules_ReturnsOkResult_WithPagedSchedules()
        {
            // Arrange
            var schedules = new PagedResult<ProgramBenefitSchedule>()
            {
                TotalRecords = 0,
                PageNumber = 1,
                PageSize = 10,
                Items = new List<ProgramBenefitSchedule>
                 {
                      new ProgramBenefitSchedule { Id = 1, ProgramCode = "NGJ", MBSItemCode = "49318", MBSScheduleFees = 49093, ProgramMedicalFees = 49094,DateOn= DateTime.Now,DateOff=DateTime.Today.AddMonths(1),ChangedBy= "dino",Comments= "Comments6",Status= "Publish"},
                      new ProgramBenefitSchedule { Id = 2, ProgramCode = "NGJ" , MBSItemCode = "00000",MBSScheduleFees =10000,ProgramMedicalFees =20000,DateOn= DateTime.Now,DateOff=DateTime.Today.AddMonths(1),ChangedBy= "xyz",Comments= "Comments2",Status= "Publish"}
                }
            };
            _serviceMock.Setup(s => s.GetPagedSchedulesAsync(null, null, true, 1, 10)).ReturnsAsync(schedules);

            // Act
            var result = await _controller.GetProgramBenefitSchedules();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnSchedules = Assert.IsType<PagedResult<ProgramBenefitSchedule>>(okResult.Value);
            Assert.Equal(returnSchedules.TotalRecords, schedules.TotalRecords);
            Assert.Equal(returnSchedules.PageNumber, schedules.PageNumber);
            Assert.Equal(returnSchedules.PageSize, schedules.PageSize);
            Assert.Equal(returnSchedules.Items, schedules.Items);
        }

        [Fact]
        public async Task GenrateDoc_ShouldReturnOkResult()
        {
            // Arrange
            var scheduleList = new List<ProgramBenefitSchedule>
            {
                new ProgramBenefitSchedule { Id = 1, ProgramCode = "P1", MBSItemCode = "M1"    }
            };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllProgramBenefitScheduleQuery>(), default))
                         .ReturnsAsync(scheduleList);

            // Act
            var result = await _controller.GenrateDoc();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Pdf Genrate", okResult.Value);
        }
    }
}
