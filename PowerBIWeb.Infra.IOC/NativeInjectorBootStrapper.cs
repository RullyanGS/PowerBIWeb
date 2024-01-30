using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PowerBIWeb.Application.Services;
using PowerBIWeb.Domain.Interfaces;
using PowerBIWeb.Domain.Validator;
using PowerBIWeb.Interfaces;
using PowerBIWeb.Models;
using PowerBIWeb.Services;

namespace PowerBIWeb.Infra.IOC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IAadService, AadService>();
            services.AddScoped<IPbiEmbedService, PbiEmbedService>();
            services.AddTransient<IValidator<(AzureAd, PowerBI)>, ConfigValidatorService>();
        }

    }
}
