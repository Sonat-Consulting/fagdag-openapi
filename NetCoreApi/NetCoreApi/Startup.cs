using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreApi.BusinessLogic;
using NetCoreApi.Configuration;
using NetCoreApi.Middleware;
using NetCoreApi.Repositories;
using NetCoreApi.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NetCoreApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var databaseConfiguration = new DatabaseConfiguration();
            Configuration.GetSection("Database").Bind(databaseConfiguration);

            services.AddSingleton(databaseConfiguration);
            services.AddSingleton<IEmployeeLogic, EmployeeLogic>();
            services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
            

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "https://sonat.eu.auth0.com/";
                options.Audience = "https://auth.sonat.dev";
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:employees",
                    policy => policy.Requirements.Add(new HasPermissionRequirements("read:employees",
                        "https://sonat.eu.auth0.com/")));
                options.AddPolicy("modify:employees",
                    policy => policy.Requirements.Add(new HasPermissionRequirements("modify:employees",
                        "https://sonat.eu.auth0.com")));
            });
            services.AddSingleton<IAuthorizationHandler, HasPermissionHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseAuthentication();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMvc();
            app.Run(async context => { await context.Response.WriteAsync("Hello World!"); });
        }
    }
}