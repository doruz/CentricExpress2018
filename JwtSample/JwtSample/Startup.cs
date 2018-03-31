using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace JwtSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity(Configuration)
                    .AddJwt(Configuration);

            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Info { Title = "JWT Sample", Version = "v1" }));

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            DatabaseMigrator.Migrate(Configuration.GetConnectionString("DefaultConnection"));

            app.UseSwagger()
               .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JWT Sample API"));

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
