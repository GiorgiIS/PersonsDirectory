using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using TBCPersonsDirectory.Repository.EF;
using TBCPersonsDirectory.Repository.Interfaces;
using TBCPersonsDirectory.Repository.Implementation;
using TBCPersonsDirectory.Services.Interfaces;
using TBCPersonsDirectory.Application;
using TBCPersonsDirectory.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace TBCPersonsDirectory.Api
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
            var assemblies = typeof(AutoMapperProfile).Assembly;
            services.AddAutoMapper(assemblies);

            var connectionString = Configuration["ConnectionStrings:Default"];
            services.AddDbContext<PersonsDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IPersonsRepository, PersonsRepository>();
            services.AddScoped<IPersonsService, PersonsService>();

            services.AddScoped<IPersonPhoneNumberRepository, PersonPhoneNumberRepository>();
            
            services.AddScoped<IPersonConnectionsRepository, PersonConnectionsRepository>();
            services.AddScoped<IReportService, ReportService>();

            services.AddOpenApiDocument();

            services.AddControllers()
                .ConfigureApiBehaviorOptions(setupAction =>
                {
                    setupAction.InvalidModelStateResponseFactory = context =>
                    {
                        var problemDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Type = "ModelValidationProblem",
                            Title = "One or more model validation errors occured",
                            Status = StatusCodes.Status422UnprocessableEntity,
                            Detail = "See the error property for details",
                            Instance = context.HttpContext.Request.Path
                        };

                        problemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);

                        return new UnprocessableEntityObjectResult(problemDetails)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    };
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseReDoc();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
