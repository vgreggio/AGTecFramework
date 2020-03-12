using AGTec.Microservice.Database;
using Correlate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace AGTec.Microservice
{
     public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseAGTecMicroservice(this IApplicationBuilder app)
        {
            return app.UseCommonAGTecMicroservice();
        }

        // With SQL Server + Migrations
        public static IApplicationBuilder UseAGTecMicroservice<TContext>(this IApplicationBuilder app) where TContext : DbContext
        {
            // Run Migrations
            using (var scope = app.ApplicationServices.CreateScope())
            {
                if (scope.ServiceProvider.GetService<TContext>() is DbContext dbContext)
                {
                    DbInitializer.Initialize(dbContext);
                }
            }

            return app.UseCommonAGTecMicroservice();
        }

        private static IApplicationBuilder UseCommonAGTecMicroservice(this IApplicationBuilder app)
        {
            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseHealthChecks("/health");

            // Adds Swagger API Doc
            var provider = app.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
            app.UseSwagger().UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });

            app.UseCorrelate();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                         .RequireAuthorization();
            });

            return app;
        }
    }
}