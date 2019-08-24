using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Business;
using Data;
using DataModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Models;
using Swashbuckle.AspNetCore.Swagger;

namespace ToDoApp
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var jwtSettingsSection = Configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSettingsSection);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {Title = "ToDo List", Version = "v1"});
                c.AddSecurityDefinition("oauth2", new ApiKeyScheme()
                {
                    Description = "This is authorization using JWT Token in format: Bearer <token>",
                    In = "header",
                    Name = "Authorization",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>()
                {
                    {"oauth2", new List<string>() }
                });
            });
            
            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
            var keyBytes = Encoding.ASCII.GetBytes(jwtSettings.Secret);

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
                    };
                });

            services.AddSingleton(Configuration);
            services.AddTransient<IRepository<User>, UserRepository>();
            services.AddTransient<IRepository<ToDoItem>, ToDoItemRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IToDoService, ToDoService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo List"); });

            app.UseMvc();
        }
    }
}
