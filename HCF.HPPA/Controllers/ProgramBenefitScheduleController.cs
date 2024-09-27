using Entities.Models;
using GrapeCity.Documents.Word;
using GrapeCity.Documents.Word.Layout;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System.Data;
using System.Globalization;
using System.IO.Compression;
using System.Reflection;

namespace HCF.HPPA.Controllers
{
    [Route("api/program-benefit-schedule")]
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
            return CreatedAtAction(nameof(Get), new { programCode = createdSchedule.ProgramCode, mbsItemCode = createdSchedule.MBSItemCode }, createdSchedule);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Int64 id, [FromBody] ProgramBenefitSchedule schedule)
        {
            if (id != schedule.Id) return BadRequest();
            _service.UpdateAsync(schedule);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Int64 id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet]
        [Route("get-pbs")]
        public async Task<IActionResult> GetProgramBenefitSchedules(
        string? search = null,
        string? sortBy = null,
        bool ascending = true,
        int pageNumber = 1,
        int pageSize = 10)
        {
            var result = await _service.GetPagedSchedulesAsync(search, sortBy, ascending, pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet]
        [Route("genrate-doc")]
        public async Task<ActionResult> GenrateDoc()
        {
            var schedulelist = await _service.GetAllAsync();
            CreateDocx(ToDataTable<ProgramBenefitSchedule>(schedulelist.ToList()));
            return Ok("Pdf Genrate");
        }

        /// <summary>
        /// Convert a List{T} to a DataTable.
        /// </summary>
        private static DataTable ToDataTable<T>(List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                tb.Columns.Add(prop.Name, Nullable.GetUnderlyingType(
            prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }
        private static GcWordDocument CreateDocx(DataTable dataTable)
        {
            var doc = new GcWordDocument();

            // Load the template DOCX:
            doc.Load(Path.Combine("Resources", "WordDocs", "Program.docx"));

            DataTable dtProds = dataTable;

            // Add the data source to the data template data sources:
            doc.DataTemplate.DataSources.Add("hospitalName", "Hurstville Private Hospital");
            doc.DataTemplate.DataSources.Add("hospitalName2", "Healthe Care Surgical Pty Ltd");
            doc.DataTemplate.DataSources.Add("section", "HCF Case Payments");
            doc.DataTemplate.DataSources.Add("effectiveDate", "Amendment effective from 1 February 2024 to 31 January 2025");
            doc.DataTemplate.DataSources.Add("ds", dtProds);

            // The document already has all the necessary bindings,
            // so we only need to process the data template:
            doc.DataTemplate.Process(CultureInfo.GetCultureInfo("en-US"));

            using (var layout = new GcWordLayout(doc))
            {
                // Define the PDF output settings
                PdfOutputSettings pdfOutputSettings = new PdfOutputSettings();
                pdfOutputSettings.CompressionLevel = CompressionLevel.Fastest;
                pdfOutputSettings.ConformanceLevel = GrapeCity.Documents.Pdf.PdfAConformanceLevel.PdfA1a;

                // Save the Word layout as a PDF
                layout.SaveAsPdf("ProcurementLetter.pdf", null, pdfOutputSettings);
            }
            // Done:
            return doc;
        }
    }
}
