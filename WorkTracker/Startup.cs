using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WorkTracker.Core.Database;
using FluentMigrator.Runner;
using WorkTracker.Core;
using LinqToDB.DataProvider.SqlServer;

namespace WorkTracker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Database
            {
                SqlServerTools.Provider = SqlServerProvider.MicrosoftDataSqlClient;

                services.AddLinqToDbContext<DbContext>((sp, options) =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("Default"))
                           .UseDefaultLogging(sp);
                });

                services.AddFluentMigratorCore()
                        .ConfigureRunner(runner =>
                        {
                            runner.AddSqlServer2016()
                                  .WithGlobalConnectionString(Configuration.GetConnectionString("Default"))
                                  .ScanIn(typeof(DbContext).Assembly).For.Migrations();
                        });
            }

            // Core
            {
                services.AddRepositories();
                services.AddEndpoints();
            }

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WorkTracker", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env,
                              IMigrationRunner migrationRunner)
        {
            // update database
            migrationRunner.MigrateUp();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WorkTracker v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
