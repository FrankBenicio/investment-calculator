using Cdb.Domain.Dto;
using Cdb.Domain.Interfaces;
using Cdb.Domain.Validators;
using Cdb.Histories;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Cdb
{
    [ExcludeFromCodeCoverage]
    public static class Cfg
    {
        public static void AddCdbInjection(this IServiceCollection services)
        {
            services.AddScoped<ICalculateCDB, CalculateCDB>();


            //Validators
            services.AddScoped<IValidator<CdbRequest>, CdbRequestValidator>();
        }
    }
}
