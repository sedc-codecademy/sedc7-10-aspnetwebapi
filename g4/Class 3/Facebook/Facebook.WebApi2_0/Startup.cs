using Facebook.WebApi2_0.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Facebook.WebApi2._0
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
            services.AddMvc(options =>
                {
                    options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                });

            services.AddSwaggerGen(options =>
                { options.SwaggerDoc("v1", new Info { Title = "Facebook api", Version = "v1" }); }
            );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseMiddleware(typeof(ErrorHandlingMiddleware));
                //app.UseExceptionHandler(options =>
                //{
                //    options.Run(
                //        async context =>
                //        {
                //            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //            context.Response.ContentType = "application/json";
                //            var ex = context.Features.Get<IExceptionHandlerFeature>();
                //            if (ex != null)
                //            {
                //                var error = new { ErrorMessage = ex.Error.Message };
                //                var errorStringified = JsonConvert.SerializeObject(error);
                //                await context.Response.WriteAsync(errorStringified).ConfigureAwait(false);
                //            }
                //        });
                //});
            }


            app.UseSwagger();
            app.UseSwaggerUI(options => 
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Facebook api v1");
            });

            app.UseMvc();
        }
    }
}
