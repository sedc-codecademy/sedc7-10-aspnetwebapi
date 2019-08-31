using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SEDC.Loto3000.BusinessLayer.Contracts;
using SEDC.Loto3000.BusinessLayer.Implementations;
using SEDC.Loto3000.DataLayer.Contracts;
using SEDC.Loto3000.DataLayer.Implementations;
using SEDC.Loto3000.Models;
using SEDC.Loto3000.Models.Settings;
using System.Text;

namespace SEDC.Loto3000.WebApi
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
            services.AddMvc();

            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IGenericRepository<User>, InMemoryGenericRepository<User>>();
            services.AddScoped<IUserRepository, InMemoryUserRepository>();

            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<ITicketRepository, InMemoryTicketRepository>();
            services.AddSingleton<IGenericRepository<Ticket>, InMemoryGenericRepository<Ticket>>();

            services.AddScoped<IDrawRepository, InMemoryDrawRepository>();
            services.AddSingleton<IGenericRepository<Draw>, InMemoryGenericRepository<Draw>>();

            services.AddScoped<IDrawService, DrawService>();

            services.AddScoped<IWinnerService, WinnerService>();
            services.AddSingleton<IGenericRepository<Winner>, InMemoryGenericRepository<Winner>>();
            services.AddScoped<IWinnerRepository, InMemoryWinnerRepository>();

            var jwtSettingsSection = Configuration.GetSection(nameof(JwtSettings));
            services.Configure<JwtSettings>(jwtSettingsSection);
            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Key);

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateAudience = false,
                        ValidateIssuer = false
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
