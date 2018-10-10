using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using YouDefine.Data;
using YouDefine.Services;
using System.Data.SqlClient;
using Hangfire;
using YouDefine.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

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
            services.Configure<CookiePolicyOptions>(options => {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<YouDefineContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("YouDefineConnection")));
            //services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection")));

            services.AddDbContext<ApplicationDbContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

    //        services.AddDefaultIdentity<IdentityUser>()
    //          .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Add application services.
            services.AddScoped<IIdeasMapper, IdeasMapper>();
            services.AddScoped<IProviderIdeas, IdeasProvider>();
            services.AddScoped<IStatisticsProvider, StatisticsProvider>();
            services.AddScoped<IStatisticsService, StatisticsService>();
            services.AddScoped<IAuthorsProvider, AuthorsProvider>();
            services.AddScoped<IWebServiceProvider, WebServiceProvider>();
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IStatisticsService statisticsService)
        {

            env.EnvironmentName = EnvironmentName.Development;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            statisticsService.InitializeStatistics();
        }
    }
}
