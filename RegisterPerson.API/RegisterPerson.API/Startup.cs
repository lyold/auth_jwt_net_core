using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AuthJWT.API.Services.Context.Implementation;
using AuthJWT.DataAccess.Abstract.Entities;
using AuthJWT.DataAccess.SqlServer.Context;
using AuthJWT.Domain.Services.Implementation;
using AuthJWT.Domain.Services.Interfaces;
using AuthJWT.Domain.Services.Security;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using Microsoft.AspNetCore.Authorization;

namespace AuthJWT.API
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
            
            #region  Registra as dependências da camada de serviços

            services.AddScoped<IUserService, UserService>();

            #endregion

            #region  Registra as dependências da camada de dados

            services.AddScoped<IUserServiceSqlServer, UserServiceSqlServer>();

            #endregion

            #region Registra o Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Serviço de Autenticação JWT .Net Core",
                    Version = "v1"
                });
            });              

            #endregion

            #region Registra o modelo de autenticação via JWT

            var signConfiguration = new SignConfigurationcs();
            services.AddSingleton(signConfiguration);
            
            var tokenConfiguration = new TokenConfigurationcs();
            
            new ConfigureFromConfigurationOptions<TokenConfigurationcs>(
                    Configuration.GetSection("TokenConfigurations")
                ).Configure(tokenConfiguration);

            services.AddSingleton(signConfiguration);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signConfiguration.Key;
                paramsValidation.ValidateAudience = string.IsNullOrEmpty(tokenConfiguration.Audience);
                paramsValidation.ValidateIssuer = string.IsNullOrEmpty(tokenConfiguration.Issuer);

                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
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

            #region Configurações Gerais
            
            app.UseHttpsRedirection();
            app.UseMvc(routes=> 
            {
                routes.MapRoute(
                        name: "Default API",
                        template: "{controllern=Values}/{id?}"
                    );
            });

            #endregion

        }
    }
}
