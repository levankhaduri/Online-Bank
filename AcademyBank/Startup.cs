using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademyBank.BLL.Services.Interfaces;
using AcademyBank.DAL;
using AcademyBank.DAL.filter;
using AcademyBank.Infrastructure;
using AcademyBank.Web.Filters;
using AcademyBank.Web.Mappings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AcademyBank.Web
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
			services.AddProjectMappings();
			services.AddProjectDatadaseConfiguration();
			services.AddProjectRepositories();
			services.AddCronos();
			services.ConfigureAuthentication(Configuration);
			services.AddControllersWithViews();
			services.AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilter)));
			services.AddMvc(options => options.Filters.Add(typeof(InformationLogFilter)));
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
				app.UseExceptionHandler("/Error");
				app.UseStatusCodePagesWithRedirects("/Error/{0}");
			}
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "/{controller=User}/{action=Login}/{id?}");

				endpoints.MapControllers();

				endpoints.MapControllerRoute(
					name: "default",
					pattern: "/{controller=Home}/{action=Index}/{id?}");
			});
			MigrateDb(app, env);
		}
		private void MigrateDb(IApplicationBuilder app, IWebHostEnvironment env)
		{
			try
			{
				using (var scope = app.ApplicationServices
				.GetRequiredService<IServiceScopeFactory>()
				.CreateScope())
				{
					var dbContextObject = scope.ServiceProvider.GetService<BankDbContext>();

					/* comment this lines if you don't need db to be droped and create each time you run the app*/
					//if (env.IsDevelopment())
					//{
					//	dbContextObject.Database.EnsureDeleted();
					//}
					//**************************************

					dbContextObject.Database.Migrate();

					var dbInitializer = scope.ServiceProvider.GetService<IDbInitializer>();
					dbInitializer.Seed().GetAwaiter().GetResult();
				}
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
