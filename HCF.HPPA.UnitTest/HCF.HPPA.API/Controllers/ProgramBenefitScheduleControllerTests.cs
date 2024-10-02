using Entities.Models;
using HCF.HPPA.Controllers;
using HCF.HPPA.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Service.Interface;

namespace HCF.HPPA.UnitTest.HCF.HPPA.API.Controllers
{
    public class ProgramBenefitScheduleControllerTests
    {
        private readonly Mock<IProgramBenefitScheduleService> _mockService;
        private readonly ProgramBenefitScheduleController _controller;

        public ProgramBenefitScheduleControllerTests()
        {
            _mockService = new Mock<IProgramBenefitScheduleService>();
            _controller = new ProgramBenefitScheduleController(_mockService.Object);
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
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(schedules);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnSchedules = Assert.IsType<List<ProgramBenefitSchedule>>(okResult.Value);
            Assert.Equal(2, returnSchedules?.Count);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenScheduleDoesNotExist()
        {
            // Arrange
            Int64 id = 1;
            _mockService.Setup(s => s.GetByIdAsync(id)).ReturnsAsync((ProgramBenefitSchedule)null);

            // Act
            var result = await _controller.GetById(id);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WithSchedule()
        {
            // Arrange
            Int64 id = 1;
            var schedule = new ProgramBenefitSchedule { Id = 1, ProgramCode = "NGJ", MBSItemCode = "49318", MBSScheduleFees = 49093, ProgramMedicalFees = 49094, DateOn = DateTime.Now, DateOff = DateTime.Today.AddMonths(1), ChangedBy = "dino", Comments = "Comments6", Status = "Publish" };

            _mockService.Setup(s => s.GetByIdAsync(id)).ReturnsAsync(schedule);

            // Act
            var result = await _controller.GetById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnSchedule = Assert.IsType<ProgramBenefitSchedule>(okResult.Value);
            Assert.Equal(id, returnSchedule.Id);
        }

        [Fact]
        public async Task Post_CreatesSchedule_ReturnsCreatedResult()
        {
            // Arrange
            var schedule = new ProgramBenefitSchedule { ProgramCode = "NGJ", MBSItemCode = "49318", MBSScheduleFees = 49093, ProgramMedicalFees = 49094, DateOn = DateTime.Now, DateOff = DateTime.Today.AddMonths(1), ChangedBy = "dino", Comments = "Comments6", Status = "Publish" };
            _mockService.Setup(s => s.AddAsync(schedule)).ReturnsAsync(schedule);

            // Act
            var result = await _controller.Post(schedule);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnSchedule = Assert.IsType<ProgramBenefitSchedule>(createdResult.Value);
            Assert.Equal(schedule.ProgramCode, returnSchedule.ProgramCode);
        }

        [Fact]
        public async Task Put_UpdatesSchedule_ReturnsNoContent()
        {
            // Arrange
            Int64 id = 1;
            var schedule = new ProgramBenefitSchedule { Id = id, ProgramCode = "NGJ", MBSItemCode = "49318", MBSScheduleFees = 49093, ProgramMedicalFees = 49094, DateOn = DateTime.Now, DateOff = DateTime.Today.AddMonths(1), ChangedBy = "dino", Comments = "Comments1", Status = "Publish" };
            _mockService.Setup(s => s.UpdateAsync(schedule));

            // Act
            var result = await _controller.Put(id, schedule);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_RemovesSchedule_ReturnsNoContent()
        {
            // Arrange
            Int64 id = 1;
            _mockService.Setup(s => s.DeleteAsync(id));

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
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
            _mockService.Setup(s => s.GetPagedSchedulesAsync(null, null, true, 1, 10)).ReturnsAsync(schedules);

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
        public async Task GenrateDoc_ReturnsOkResult()
        {
            // Arrange
            var schedules = new List<ProgramBenefitSchedule>
        {
            new ProgramBenefitSchedule { Id = 1, ProgramCode = "P1", MBSItemCode = "M1" }
        };
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(schedules);

            // Act
            var result = await _controller.GenrateDoc();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Pdf Genrate", okResult.Value);
        }
    }
}
