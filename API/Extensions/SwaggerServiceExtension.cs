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
            services.AddSwaggerGen(c =>
       {
           c.SwaggerDoc("v1", new OpenApiInfo { Title = "Amma Ki Rashoi", Version = "v1" });
           var securitySchema = new OpenApiSecurityScheme{
               Description="JWT Auth Bearer Scheme",
               Name ="Authorization",
               In= ParameterLocation.Header,
               Type=SecuritySchemeType.Http,
               Scheme= "bearer",
               Reference= new OpenApiReference{
                   Type=ReferenceType.Schema,
                   Id="Bearer"
               }
           };
           c.AddSecurityDefinition("Bearer",securitySchema);
           var securityRequirement = new OpenApiSecurityRequirement{
               {securitySchema,new []{"Brearer"}}
           };
        //    c.AddSecurityRequirement(securityRequirement);

       });

            return services;
        }
        public static IApplicationBuilder UseSwaggerDocumentation(this
         IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "AmmaKiRashoi API V1"); });

            return app;
        }
    }
}