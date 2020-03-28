using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RegisterPerson.API.Services.Context.Implementation;
using RegisterPerson.DataAccess.Abstract.Entities;
using RegisterPerson.DataAccess.SqlServer.Context;
using RegisterPerson.Domain.Services.Implementation;
using RegisterPerson.Domain.Services.Interfaces;

namespace RegisterPerson.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #region Registra o banco de dados

            var connection = Configuration["SQLServerConnection:SQLServerConnectionString"];
            services.AddDbContext<SQLServerContext>(options => options.UseSqlServer(connection));

            #endregion

            #region  Registra as dependências

            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPersonServiceSqlServer, PersonServiceSqlServer>();
            
            #endregion

            #region Registra o Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "RestFul API with .NET Core 2.0",
                    Version = "v1"
                });
            });              

            #endregion
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
                app.UseHsts();
            }

            #region Configurações do Swagger

            app.UseSwagger();

            app.UseSwaggerUI(s => {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            #endregion
            
            app.UseHttpsRedirection();
            app.UseMvc(routes=> 
            {
                routes.MapRoute(
                        name: "Default API",
                        template: "{controllern=Values}/{id?}"
                    );
            });
            

        }
    }
}
