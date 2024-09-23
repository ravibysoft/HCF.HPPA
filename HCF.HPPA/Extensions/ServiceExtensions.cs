using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Interface;
using Service;
using Service.Interface;

namespace CompanyEmployees.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RepositoryContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IProgramBenefitScheduleRepository, ProgramBenefitScheduleRepository>();
        services.AddScoped<IProgramBenefitScheduleService, ProgramBenefitScheduleService>();

        services.AddControllers();
    }
}
