using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var host = new HostBuilder()
           .ConfigureFunctionsWebApplication(builder =>
           {
               builder.UseNewtonsoftJson();
           })
           .ConfigureServices((hostContext, services) =>
           {
               services.AddSingleton<IOpenApiConfigurationOptions>(_ =>
               {
                   var options = new OpenApiConfigurationOptions
                   {
                       Info = new OpenApiInfo
                       {
                           Version = OpenApiVersionType.V3.ToString(),
                           Title = "Salesforce Function App",
                           Description = "Salesforce API Function App",
                       },
                       OpenApiVersion = OpenApiVersionType.V3,
                       Servers = []
                   };
                   return options;
               });
           })
           .Build();

host.Run();