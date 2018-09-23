using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using YouDefine.Data;
using YouDefine.Services;
using System.Data.SqlClient;
//using Hangfire;

namespace YouDefine
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
            services.AddMvc();
            services.AddScoped<IIdeasMapper, IdeasMapper>();
            services.AddScoped<IProviderIdeas, IdeasProvider>();
            services.AddScoped<IStatisticsProvider, StatisticsProvider>();
            services.AddScoped<IStatisticsService, StatisticsService>();
            services.AddScoped<IAuthorsProvider, AuthorsProvider>();
            services.AddScoped<IWebServiceProvider, WebServiceProvider>();

            //services.AddDbContext<YouDefineContext>(
            //    opt => opt.UseInMemoryDatabase("YouDefineContext")
            //);
            services.AddDbContext<YouDefineContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddHangfire(x => x.UseSqlServerStorage("HangfireConnection"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IStatisticsService statistics)
        {

            //env.EnvironmentName = EnvironmentName.Production;

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            //app.UseStatusCodePages("text/plain", "Status code page, status code: {0}");
            //app.UseHangfireServer();
            //app.UseHangfireDashboard();
            app.UseStaticFiles();
            app.UseDefaultFiles();

            statistics.InitializeStatistics();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
