using Microsoft.Extensions.DependencyInjection;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection PersistenceDependencyInjection(this IServiceCollection services)
        {
            //services
            //.AddScoped<IUnitOfWork, UnitOfWork>()
            //.AddScoped<IReportRepository, ReportRepository>();
            return services;
        }
    }
}