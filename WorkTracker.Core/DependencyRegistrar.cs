using Microsoft.Extensions.DependencyInjection;
using WorkTracker.Core.Database.Repositories.RecordRepo;
using WorkTracker.Core.Endpoints;
using WorkTracker.Core.Records;

namespace WorkTracker.Core
{
    public static class DependencyRegistrar
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRecordRepository, RecordRepository>();

            return services;
        }

        public static IServiceCollection AddEndpoints(this IServiceCollection services)
        {
            services.AddScoped<IEndpoint<CreateRecord, Record>, CreateRecordEndpoint>();

            return services;
        }
    }
}
