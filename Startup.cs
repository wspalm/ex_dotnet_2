using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using midterm_6013532.Data;
using midterm_6013532.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
namespace midterm_6013532
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
            
            
            services.AddIdentity<AppUser,AppRole>()
             .AddEntityFrameworkStores<midterm_6013532DbContext>()
             .AddDefaultTokenProviders()
             .AddDefaultUI();            
            
            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = false;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                 
            });
           
            //setup db
			services.AddDbContextPool<midterm_6013532DbContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(
                        Configuration.GetConnectionString("DefaultConnection"),
                        new MySqlServerVersion(new Version(8, 0, 21)), 
                        mySqlOptions => mySqlOptions
                        .CharSetBehavior(CharSetBehavior.NeverAppend)
                    )  
			    );

            services.AddSwaggerGen(c =>
            {
                c.ResolveConflictingActions (apiDescriptions => apiDescriptions.First ());
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "project api version1", Version = "v1" });
            });	
            //setup mvc and cshtml runtime compliation
            services.AddControllersWithViews()
            .AddRazorRuntimeCompilation()
            .AddNewtonsoftJson(options =>
            	options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );    
            //add support for razor page
            services.AddRazorPages(); 
        }//ef

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,midterm_6013532DbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            

            
				
            app.UseHttpsRedirection();

            
            app.UseSwagger();
          app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "project api version1"));
	

  
  			app.UseStaticFiles();
  
            app.UseRouting();
			app.UseAuthentication();

  			app.UseAuthorization(); 

			 
			 app.UseEndpoints(endpoints =>
            {  
                endpoints.MapRazorPages();//razor page mapping
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");   
            });

        }//ef

		 
         


    }//ec
}//en
