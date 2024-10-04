using DocumentFormat.OpenXml.Office2010.Excel;
using GrapeCity.Documents.Word;
using GrapeCity.Documents.Word.Layout;
using HCF.HPPA.Common.Models;
using HCF.HPPA.Domain.Commands;
using HCF.HPPA.Domain.Queries;
using HCF.HPPA.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Globalization;
using System.IO.Compression;
using System.Reflection;

namespace HCF.HPPA.API.Controllers
{
    [Route("api/program-benefit-schedule")]
    [ApiController]
    public class ProgramBenefitScheduleController : ControllerBase
    {
        private readonly ILogger<ProgramBenefitScheduleController> _logger;
        private readonly IProgramBenefitScheduleService _service;
        private readonly IMediator _mediator;

        public ProgramBenefitScheduleController(ILogger<ProgramBenefitScheduleController> logger,IProgramBenefitScheduleService service, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new GetAllProgramBenefitScheduleQuery());
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            _logger.LogInformation($"find data using {id}");
            var schedule = await _mediator.Send(new GetByIdProgramBenefitScheduleQuery() { Id = id });
            if (schedule == null) return BadRequest();
            _logger.LogInformation($"data find using this {id}");
            return Ok(schedule);
        }

        [HttpPost]
        public async Task<ActionResult<ProgramBenefitSchedule>> Post([FromBody] CreateProgramBenefitScheduleCommand schedule)
         {
            if (schedule is null) return BadRequest();

            var createdSchedule = await _mediator.Send(schedule);

            return CreatedAtAction(nameof(Get), createdSchedule);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(long id, [FromBody] UpdateProgramBenefitScheduleCommand schedule)
        {
            if (id != schedule.Id) return BadRequest();

            var createdSchedule = await _mediator.Send(schedule);
            return Ok(createdSchedule);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var response = await _mediator.Send(new DeleteProgramBenefitScheduleCommand()
            {
                Id = id
            });
            return Ok(response);
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
            var schedulelist = await _mediator.Send(new GetAllProgramBenefitScheduleQuery());
            CreateDocx(ToDataTable(schedulelist.ToList()));
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
