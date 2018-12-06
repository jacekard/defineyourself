namespace YouDefine
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using YouDefine.Data;
    using YouDefine.Services;
    using System.Data.SqlClient;
    using YouDefine.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    //using Hangfire;

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
            services.AddMvc();
            services.AddScoped<IIdeasMapper, IdeasMapper>();
            services.AddScoped<IProviderIdeas, IdeasProvider>();
            services.AddScoped<IStatisticsProvider, StatisticsProvider>();
            services.AddScoped<IStatisticsService, StatisticsService>();
            services.AddScoped<IAuthorsProvider, AuthorsProvider>();
            services.AddScoped<IWebServiceProvider, WebServiceProvider>();
            services.AddDbContext<YouDefineContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddHangfire(x => x.UseSqlServerStorage("HangfireConnection"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            //env.EnvironmentName = EnvironmentName.Production;

            if (env.IsDevelopment())
            {
                app.UseDatabaseErrorPage();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            ///*****************************************
            /// Uncomment Hangfire Server and Dashboard
            /// to use Hangfire Provider!
            //  app.UseHangfireServer();
            //  app.UseHangfireDashboard();
            ///*****************************************

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
