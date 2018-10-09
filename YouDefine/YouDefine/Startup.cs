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
using Hangfire;

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
            services.AddDbContext<YouDefineContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("YouDefineConnection")));
            //services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection")));

            services.AddScoped<IIdeasMapper, IdeasMapper>();
            services.AddScoped<IProviderIdeas, IdeasProvider>();
            services.AddScoped<IStatisticsProvider, StatisticsProvider>();
            services.AddScoped<IStatisticsService, StatisticsService>();
            services.AddScoped<IAuthorsProvider, AuthorsProvider>();
            services.AddScoped<IWebServiceProvider, WebServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IStatisticsService statisticsService)
        {

            env.EnvironmentName = EnvironmentName.Development;

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            //app.UseHangfireServer();
            //app.UseHangfireDashboard();
            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            statisticsService.InitializeStatistics();
        }
    }
}
