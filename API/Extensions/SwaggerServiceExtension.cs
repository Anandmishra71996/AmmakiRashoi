using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API.Extensions
{
    public static class SwaggerServiceExtension
    {
        public static IServiceCollection AddSwaggerDocumentaion(
            this IServiceCollection services)
            {
                 services.AddSwaggerGen(c=>
            {c.SwaggerDoc("v1",new OpenApiInfo{ Title="Amma Ki Rashoi", Version="v1"});
            });

            return services;
            }
        public static IApplicationBuilder UseSwaggerDocumentation(this
         IApplicationBuilder app)
         {
             app.UseSwagger();
            app.UseSwaggerUI(c =>{c.SwaggerEndpoint("/swagger/v1/swagger.json","AmmaKiRashoi API V1");});

            return app;
         }
    }
}