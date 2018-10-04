using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using burgershack.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace burgershack
{
    public class Startup
    {
        private readonly string _connectionString = "";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _connectionString = configuration.GetSection("DB").GetValue<string>("mySQLConnectionString");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //User Auth
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.Events.OnRedirectToLogin = (context) => {
                        context.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    };
                });

            //CORS
            services.AddCors(options => {
                options.AddPolicy("CorsDevPolicy", builder => {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                });
            });
            services.AddMvc();

            services.AddTransient<IDbConnection>(x => CreateDBContext()); //this method generates the connectionStr equivalent in Node/mongoose
            services.AddTransient<BurgersRepository>(); //add transient will open a connection to the db, use it as long as it needs to, and then tears it down. This is far safer then leaving the connection always open. It's also cheaper.
            services.AddTransient<SmoothiesRepository>();
            services.AddTransient<UserRepository>();
            //equivalent to node's let bp = require("body-parser") etc...
            //this is where pull in third party programs and use them
            
            // services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        private IDbConnection CreateDBContext()
        {
            var connection = new MySqlConnection(_connectionString); //connection not connector! important
            connection.Open();
            return connection;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //equivalent to node's app.use(bp)
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("CorsDevPolicy");
            }
            else
            {
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseStaticFiles(); //by default will always look for wwwroot folder
            app.UseMvc();
        }
    }
}
