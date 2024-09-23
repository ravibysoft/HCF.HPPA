using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace HCF.HPPA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramBenefitScheduleController : ControllerBase
    {
        private readonly IProgramBenefitScheduleService _service;

        public ProgramBenefitScheduleController(IProgramBenefitScheduleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgramBenefitSchedule>>> Get()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet]
        [Route("GetByProgramCodeAndMBSItemCode")]
        public async Task<ActionResult<ProgramBenefitSchedule>> Get(string programCode , string mbsItemCode)
        {
            var schedule = await _service.GetByProgramCodeAndMBSItemCodeAsync(programCode, mbsItemCode);
            if (schedule == null) return NotFound();
            return Ok(schedule);
        }

        [HttpPost]
        public async Task<ActionResult<ProgramBenefitSchedule>> Post([FromBody] ProgramBenefitSchedule schedule)
        {
            var createdSchedule = await _service.AddAsync(schedule);
            return CreatedAtAction(nameof(Get), new { programCode = createdSchedule.ProgramCode , mbsItemCode = createdSchedule.MBSItemCode }, createdSchedule);
        }

        [HttpPut]
        [Route("UpdateSchedule")]
        public async Task<ActionResult> Put(string programCode, string mbsItemCode, [FromBody] ProgramBenefitSchedule schedule)
        {
            if (programCode != schedule.ProgramCode) return BadRequest();
            await _service.UpdateAsync(programCode, mbsItemCode,schedule);
            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteSchedule")]
        public async Task<ActionResult> Delete(string programCode, string mbsItemCode)
        {
            var deleted = await _service.DeleteAsync(programCode,mbsItemCode);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
