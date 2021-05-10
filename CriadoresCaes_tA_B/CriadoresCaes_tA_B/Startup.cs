using CriadoresCaes_tA_B.Data;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// adicionar esta refer�ncia para q a EF seja invocada
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CriadoresCaes_tA_B {
   public class Startup {
      public Startup(IConfiguration configuration) {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }




      // This method gets called by the runtime.
      // Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services) {

         // uso de vars. de sess�o
         services.AddDistributedMemoryCache();
         services.AddSession(options => {
            options.IdleTimeout = TimeSpan.FromSeconds(100);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
         });

         services.AddControllersWithViews();

         //************************************************************
         // configurar o acesso � BD
         //************************************************************
         services.AddDbContext<CriadoresCaesDB>(
            options => options.UseSqlServer( Configuration.GetConnectionString("myConnectionString")  )
            );

         //Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 5.0.4

         //************************************************************


      }




      // This method gets called by the runtime.
      // Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {

         if (env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
         }
         else {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
         }
         app.UseHttpsRedirection();
         app.UseStaticFiles();

         app.UseRouting();

         // permitir o uso de vars. de sess�o
         app.UseSession();

         app.UseAuthorization();

         app.UseEndpoints(endpoints => {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
         });
      }
   }
}
