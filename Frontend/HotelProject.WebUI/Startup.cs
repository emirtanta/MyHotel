using FluentValidation;
using FluentValidation.AspNetCore;
using HotelProject.DataAccessLayer.Concrete;
using HotelProject.EntityLayer.Concrete;
using HotelProject.WebUI.Dtos.GuestDto;
using HotelProject.WebUI.ValidationRules.GuestValidationRules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.WebUI
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
            //UserManager tan�m�
            services.AddDbContext<Context>();
            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>();

            services.AddHttpClient(); //api frontend taraf�nda kullanmay� sa�lar

            //fluent validation
            services.AddTransient<IValidator<CreateGuestDto>,CreateGuestValidator>();
            services.AddTransient<IValidator<UpdateGuestDto>,UpdateGuestValidator>();

            services.AddControllersWithViews().AddFluentValidation();

            //autoMapper tan�mland�
            services.AddAutoMapper(typeof(Startup));

            //proje �zerinde authorize i�lemi. Bu i�lem yap�ld���nda AllowAnnoymous attribute'si olmayan sayfalar g�z�kmez (iste�e ba�l�)
            //services.AddMvc(config =>
            //{
            //    var policy = new AuthorizationPolicyBuilder()
            //    .RequireAuthenticatedUser()
            //    .Build();

            //    config.Filters.Add(new AuthorizeFilter(policy));
            //});

            //login path ayar�
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan= TimeSpan.FromMinutes(10); //10dakika sonra tekrar login olmak zorunda olur
                options.LoginPath = "/Login/Index"; //giri� yap�lmad���nda gidilecek sayfa
            });

            //proje �zerinde authorize i�lemi (iste�e ba�l�) bitti
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
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404","?code={0}"); //sayfalar bulunamazsa error sayfas�na y�nlendirir
            app.UseHttpsRedirection(); //sayfalar bulunamazsa error sayfas�na y�nlendirir

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
