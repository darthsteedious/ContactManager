using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ContactManager.Entities;
using ContactManager.Repositories;
using System.Data;
using ContactManager.Validation;
using Npgsql;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Runtime.InteropServices.ComTypes;

namespace ContactManager
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
            
            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            });

            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IDbConnection, NpgsqlConnection>(p => {
                var connectionString = Environment.GetEnvironmentVariable("POSTGRES_CN_STRING");
                var connection = new NpgsqlConnection(connectionString);
                
                connection.Open();

                return connection;
            });

            services.AddTransient<IValidator<User>, UserValidator>();
            services.AddTransient<IValidator<Contact>, ContactValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) =>
            {
                context.Items.Add("user_context", new UserContext { Id = 1 });

                await next.Invoke();
            });
            app.UseMvc();
            
        }
    }
}
