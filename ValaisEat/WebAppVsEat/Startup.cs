using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using DAL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebAppVsEat
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //Add the services of the session to share data between controllers and views
            services.AddSession();

            //Service for our login and logout
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
         options =>
         {
             options.Cookie.HttpOnly = true;
             options.SlidingExpiration = true;
             options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
             options.LoginPath = new PathString("/Login/login");
             options.AccessDeniedPath = new PathString("/Home/Index");
         });

            //Initiate all the class for Data Access
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IUserDB, UserDB>();

            services.AddScoped<ICustomerManager, CustomerManager>();
            services.AddScoped<ICustomerDB, CustomerDB>();

            services.AddScoped<ICityManager, CityManager>();
            services.AddScoped<ICityDB, CityDB>();

            services.AddScoped<IRestaurantManager, RestaurantManager>();
            services.AddScoped<IRestaurantDB, RestaurantDB>();

            services.AddScoped<IDishManager, DishManager>();
            services.AddScoped<IDishDB, DishDB>();

            services.AddScoped<IOrderManager, OrderManager>();
            services.AddScoped<IOrderDB, OrderDB>();

            services.AddScoped<IOrder_DishesManager, Order_DishesManager>();
            services.AddScoped<IOrder_DishesDB, Order_DishesDB>();

            services.AddScoped<ICourierManager, CourierManager>();
            services.AddScoped<ICourierDB, CourierDB>();



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
        }







        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSession();

            app.UseAuthentication();
            

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
