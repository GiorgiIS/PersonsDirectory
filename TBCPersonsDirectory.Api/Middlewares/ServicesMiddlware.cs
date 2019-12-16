using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TBCPersonsDirectory.Application;
using TBCPersonsDirectory.Repository.Implementation;
using TBCPersonsDirectory.Repository.Interfaces;
using TBCPersonsDirectory.Services.Interfaces;

namespace TBCPersonsDirectory.Api.Middlewares
{
    public static class ServicesMiddlwares
    {
        public static IServiceCollection AddServicesAndRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPersonsRepository, PersonsRepository>();
            services.AddScoped<IPersonsService, PersonsService>();

            services.AddScoped<IPersonPhoneNumberRepository, PersonPhoneNumberRepository>();

            services.AddScoped<IPersonConnectionsRepository, PersonConnectionsRepository>();
            services.AddScoped<IReportService, ReportService>();

            services.AddScoped<ICitysService, CitysService>();

            services.AddScoped<IPictureUploader, PictureUploader>();
            services.AddScoped<IPictureService, PictureService>();

            return services;
        }
    }
}
