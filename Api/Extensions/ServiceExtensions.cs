using HCF.HPPA.Domain.Services;
using HCF.HPPA.Repository.Models;
using HCF.HPPA.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HCF.HPPA.API.Extensions;

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
