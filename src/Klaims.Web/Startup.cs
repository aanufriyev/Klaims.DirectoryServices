using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Diagnostics.Entity;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Security.Cookies;
using Microsoft.Data.Entity;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;

namespace Klaims.Web
{
	using System.Linq;

	using Klaims.Framework.IdentityMangement;
	using Klaims.Framework.IdentityMangement.Models;
	using Klaims.Scim.Query;
	using Klaims.Scim.Rest;
	using Klaims.Scim.Rest.Formatters;
	using Klaims.Scim.Services;

	using Microsoft.AspNet.Mvc;

	using Newtonsoft.Json;
	using Newtonsoft.Json.Serialization;

	public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Setup configuration sources.
            Configuration = new Configuration()
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime.
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddMvc().Configure<MvcOptions>(options =>
			{
				options.OutputFormatters.Add(new ScimJsonOutputFormatter());
			});

	        services.AddTransient<IScimUserManager, DefaultScimUserManager>();
			services.AddTransient<IUserAccountManager<User>, DefaultUserAccountManager>();
			services.AddTransient<IUserAccountRepository<User>, InMemoryUserAccountRepository>();
			services.AddTransient<IFilterBinder, DefaultFilterBinder>();
			services.AddTransient<IAttributeNameMapper, DefaultAttributeNameMapper>();
		}

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory)
        {
            // Configure the HTTP request pipeline.
            // Add the console logger.
            loggerfactory.AddConsole();

            // Add the following to the request pipeline only in development environment.
            if (string.Equals(env.EnvironmentName, "Development", StringComparison.OrdinalIgnoreCase))
            {
                //app.UseBrowserLink();
                app.UseErrorPage(ErrorPageOptions.ShowAll);
                app.UseDatabaseErrorPage(DatabaseErrorPageOptions.ShowAll);
            }
            else
            {
                // Add Error handling middleware which catches all application specific errors and
                // send the request to the following path or controller action.
                app.UseErrorHandler("/Home/Error");
            }

            // Add static files to the request pipeline.
            app.UseStaticFiles();

            // Add cookie-based authentication to the request pipeline.
            app.UseIdentity();

            // Add MVC to the request pipeline.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });

                // Uncomment the following line to add a route for porting Web API 2 controllers.
                // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
            });
        }
    }
}
