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

        [HttpGet("{id}")]
        public async Task<ActionResult<ProgramBenefitSchedule>> GetById(Int64 id)
        {
            var schedule = await _service.GetByIdAsync(id);
            if (schedule == null) return NotFound();
            return Ok(schedule);
        }

        [HttpPost]
        public async Task<ActionResult<ProgramBenefitSchedule>> Post([FromBody] ProgramBenefitSchedule schedule)
        {
            var createdSchedule = await _service.AddAsync(schedule);
            return CreatedAtAction(nameof(Get), new { programCode = createdSchedule.ProgramCode , mbsItemCode = createdSchedule.MBSItemCode }, createdSchedule);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Int64 id, [FromBody] ProgramBenefitSchedule schedule)
        {
            if (id != schedule.Id) return BadRequest();
            await _service.UpdateAsync(schedule);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Int64 id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
