using Entities.Models;
using HCF.HPPA.Entities.Models;
using Moq;
using Repository.Interface;
using Service;

namespace HCF.HPPA.UnitTest.HCF.HPPA.API.Service
{
    public class ProgramBenefitScheduleServiceTests
    {
        private readonly Mock<IProgramBenefitScheduleRepository> _mockRepository;
        private readonly ProgramBenefitScheduleService _service;

        public ProgramBenefitScheduleServiceTests()
        {
            _mockRepository = new Mock<IProgramBenefitScheduleRepository>();
            _service = new ProgramBenefitScheduleService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllSchedules()
        {
            // Arrange
            var schedules = new List<ProgramBenefitSchedule>
        {
            new ProgramBenefitSchedule { Id = 1, ProgramCode = "NGJ", MBSItemCode = "49318", MBSScheduleFees = 49093, ProgramMedicalFees = 49094,DateOn= DateTime.Now,DateOff=DateTime.Today.AddMonths(1),ChangedBy= "dino",Comments= "Comments6",Status= "Publish"},
            new ProgramBenefitSchedule { Id = 2, ProgramCode = "NGJ" , MBSItemCode = "00000",MBSScheduleFees =10000,ProgramMedicalFees =20000,DateOn= DateTime.Now,DateOff=DateTime.Today.AddMonths(1),ChangedBy= "xyz",Comments= "Comments2",Status= "Publish"}
        };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(schedules);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(schedules, result);
        }

        [Fact]
        public async Task GetByIdAsync_ValidId_ShouldReturnSchedule()
        {
            // Arrange
            var schedule = new ProgramBenefitSchedule { Id = 1, ProgramCode = "NGJ", MBSItemCode = "49318", MBSScheduleFees = 49093, ProgramMedicalFees = 49094, DateOn = DateTime.Now, DateOff = DateTime.Today.AddMonths(1), ChangedBy = "dino", Comments = "Comments6", Status = "Publish" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(schedule);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.Equal(schedule, result);
        }

        [Fact]
        public async Task AddAsync_ValidSchedule_ShouldReturnAddedSchedule()
        {
            // Arrange
            var newSchedule = new ProgramBenefitSchedule { ProgramCode = "NGJ", MBSItemCode = "49318", MBSScheduleFees = 49093, ProgramMedicalFees = 49094, DateOn = DateTime.Now, DateOff = DateTime.Today.AddMonths(1), ChangedBy = "dino", Comments = "Comments6", Status = "Publish" };
            _mockRepository.Setup(repo => repo.AddAsync(newSchedule)).ReturnsAsync(newSchedule);

            // Act
            var result = await _service.AddAsync(newSchedule);

            // Assert
            Assert.Equal(newSchedule, result);
        }

        [Fact]
        public void UpdateAsync_ValidSchedule_ShouldCallRepositoryUpdate()
        {
            // Arrange
            var scheduleToUpdate = new ProgramBenefitSchedule { Id = 1, ProgramCode = "NGJ", MBSItemCode = "49318", MBSScheduleFees = 49093, ProgramMedicalFees = 49095 };

            // Act
            _service.UpdateAsync(scheduleToUpdate);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(scheduleToUpdate), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ValidId_ShouldReturnTrue()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.DeleteAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetPagedSchedulesAsync_ShouldReturnPagedSchedules()
        {
            // Arrange
            var pagedResult = new PagedResult<ProgramBenefitSchedule>
            {
                Items = new List<ProgramBenefitSchedule>
            {
                 new ProgramBenefitSchedule { Id = 1, ProgramCode = "NGJ", MBSItemCode = "49318", MBSScheduleFees = 49093, ProgramMedicalFees = 49094,DateOn= DateTime.Now,DateOff=DateTime.Today.AddMonths(1),ChangedBy= "dino",Comments= "Comments6",Status= "Publish"},
                 new ProgramBenefitSchedule { Id = 2, ProgramCode = "NGJ" , MBSItemCode = "00000",MBSScheduleFees =10000,ProgramMedicalFees =20000,DateOn= DateTime.Now,DateOff=DateTime.Today.AddMonths(1),ChangedBy= "xyz",Comments= "Comments2",Status= "Publish"}
            },
                TotalRecords = 1,
                PageNumber = 1,
                PageSize = 10,
            };

            _mockRepository.Setup(repo => repo.GetPagedAsync(null, null, true, 1, 10))
                .ReturnsAsync(pagedResult);

            // Act
            var result = await _service.GetPagedSchedulesAsync();

            // Assert
            Assert.Equal(pagedResult, result);
        }
    }
}
