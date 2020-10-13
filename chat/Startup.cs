using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chat.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;


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

            services.Configure<IdentityOptions>(options =>
            options.Password.RequireNonAlphanumeric = false
            );

            services.AddDbContextPool<AppDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("FirstDataBase")));

            services.AddControllersWithViews().AddXmlSerializerFormatters();
            services.AddTransient<IChatRepository, SqlChatRepository>();
            services.AddTransient<IAppUserChatRepository, SqlAppUserChatRepository>();
            services.AddTransient<IMessageRepository, SqlMessageRepository>();

            services.AddRazorPages();

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();
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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=MainMenu}/{action=Hello}/{id?}");
            });
        }
    }
}
