using chat.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using chat.Hubs;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Localization.Routing;

namespace chat
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
            services.AddRazorPages();

            services.Configure<IdentityOptions>(options =>
            options.Password.RequireNonAlphanumeric = false);

            services.AddDbContextPool<AppDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("FirstDataBase")));

            services.AddControllersWithViews().AddXmlSerializerFormatters();

            services.AddSignalR();

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                //options.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc().AddViewLocalization();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] {
                    new CultureInfo("en-us"), 
                    new CultureInfo("uk-ua"), 
                    new CultureInfo("de-de") 
                };

                options.DefaultRequestCulture = new RequestCulture(culture: "en-us", uiCulture: "en-us");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders.Insert(0,
                    new RouteDataRequestCultureProvider { Options = options });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseRouting();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                var localizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
                app.UseRequestLocalization(localizationOptions.Value);

                endpoints.MapControllerRoute(
                    name: "DefaultWithCulture",
                    pattern: "{culture=en-us}/{controller=Chat}/{action=AllChats}/{id?}"        
                    ); 

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Chat}/{action=AllChats}/{id?}");

                endpoints.MapHub<ChatHub>("/chathub");
            });
        }
    }
}
