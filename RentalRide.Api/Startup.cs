using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalRide.Api.Security;
using RentalRide.Infra.CrossCutting.IoC;
using RentalRide.Infra.CrossCutting.AspNetFilters;

namespace RentalRide.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc(config =>
            {
                config.Filters.Add(new ServiceFilterAttribute(typeof(GlobalExceptionHandlingFilter)));
            });

            services.AddResponseCompression();

            services.AddCors();

            RegisterServices(services);

            ConfigureJwtAuthService(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "RentalRide - Api",
                    Version = "v1",
                    Description = "Documentation containing information on RentalRide"
                });

            });
        }

        public void Configure(
            IApplicationBuilder app,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
            
            
            app.UseStaticFiles();

            app.UseResponseCompression();

            app.UseCors(x => { x.AllowAnyHeader(); x.AllowAnyOrigin(); x.AllowAnyMethod(); });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RentalRide API");
            });

            app.Run(async (context) =>
            {
                context.Response.Redirect("swagger");
                await Task.CompletedTask;
            });
        }

        public void ConfigureJwtAuthService(IServiceCollection services)
        {
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);
            var tokenConfigurations = new TokenConfigurations();

            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.AUDIENCE;
                paramsValidation.ValidIssuer = tokenConfigurations.ISSUER;

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
        }

        private static void RegisterServices(IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
