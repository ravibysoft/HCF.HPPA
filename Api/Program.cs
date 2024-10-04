using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using HCF.HPPA.API.Extensions;
using HCF.HPPA.Domain.AutoMapper;
using HCF.HPPA.Domain.CommandHandlers;
using HCF.HPPA.Domain.QueryHandlers;
using Serilog;
using System.Configuration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
       .AddJsonFile("appsettings.Development.json", optional: false)
       .Build();
// Add services to the container.

//Add support to logging with SERILOG
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Host.UseSerilog(logger);
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(CommonMappingProfile).GetTypeInfo().Assembly);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllProgramBenefitScheduleQueryHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetByIdProgramBenefitScheduleQueryHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeleteProgramBenefitScheduleCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProgramBenefitScheduleCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateProgramBenefitScheduleCommandHandler).Assembly));


IConfigurationSection myArraySection = config.GetSection("AllowedOrigins");
var itemArray = myArraySection.AsEnumerable();
var allowedOrigins = itemArray.Where(x => x.Value != null).Select(x => x.Value).ToArray();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.WithOrigins(allowedOrigins as string[])
                .SetIsOriginAllowedToAllowWildcardSubdomains()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddSwaggerGen();
builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAllOrigins");

//Add support to logging request with SERILOG
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
