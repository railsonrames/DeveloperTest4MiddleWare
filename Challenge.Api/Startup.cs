using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Challenge.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }
        readonly string AllowedSpecificOrigins = "_AllowedSpecificOrigins";
        readonly string[] SpecifcUrlOriginsList = { "http://localhost:9000" };
        readonly string[] SpecificHtmlVerbs = { "GET" };

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => {
                options.AddPolicy(AllowedSpecificOrigins, builder =>
                    builder.WithOrigins(SpecifcUrlOriginsList)
                           .WithMethods(SpecificHtmlVerbs));
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCors(AllowedSpecificOrigins);
            app.UseMvc();
        }
    }
}
