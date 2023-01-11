using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PauliTicket.Application.Contracts.Infrastructure;
using PauliTicket.Application.Models.Mail;
using PauliTicket.Infrastructure.FileExport;
using PauliTicket.Infrastructure.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauliTicket.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

            services.AddTransient<IEmailService, EmailService>();

            services.AddTransient<ICsvExporter, CsvExporter>();

            return services;
        }
    }
}
