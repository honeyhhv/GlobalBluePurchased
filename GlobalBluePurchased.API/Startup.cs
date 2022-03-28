using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using GlobalBluePurchased.API.Common;
using GlobalBluePurchased.Domain.Core.Models;
using GlobalBluePurchased.Domain.Handler;
using GlobalBluePurchased.Domain.Request;
using GlobalBluePurchased.Domain.ResourceManagers;
using GlobalBluePurchased.Domain.Resources;
using GlobalBluePurchased.Domain.Resources.ResourceManagers.Interface;
using MediatR;
using Microsoft.AspNetCore.Mvc.Razor;

namespace GlobalBluePurchased.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddLocalization(opt => opt.ResourcesPath = "Resources");
            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CalculatePurchaseValidator>())
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                    factory.Create(typeof(SharedResource));
            });
            services.AddValidatorsFromAssembly(ServiceAssembly.Current);

            services.AddScoped<IRequestHandler<CalculatePurchaseQueries, ResultDto>, CalculatePurchaseQueriesHandler>();


            services.AddScoped<IResourceManager, ResourceManager<SharedResource>>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GlobalBluePurchased.API", Version = "v1" });
            });
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
            services.AddMediatR(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GlobalBluePurchased.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseRequestLocalization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
