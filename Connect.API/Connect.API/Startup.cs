using Connect.API.Hubs;
using Connect.API.Infrastructure;
using Connect.API.Models.Configuration;
using Connect.API.Services;
using Connect.Interface.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace connect.api
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

            services.AddControllers();
            services.AddSignalR();


            services.AddCors(options => options.AddPolicy(ConnectConstants.CORSS_POLICY_NAME,
               builder =>
               {
                   builder
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .SetIsOriginAllowed((host) => true)
                   //.WithOrigins("http://10.10.15.143:4200")
                   .AllowCredentials();
               }));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Connect The Planet API",
                    Version = "v1.0",
                    Description = "The connect the planet will be a messaging system where users can be signed up and immediately chatting with each other.",
                    Contact = new OpenApiContact { Name = "Mahi", Email = "mahi.tshooter@gmail.com", Url = new Uri("https://www.linkedin.com/in/kingcobrass/") }
                });
                c.ResolveConflictingActions(apiDescription => apiDescription.First());

                c.IncludeXmlComments(System.IO.Path.Combine(System.AppContext.BaseDirectory, "Connect.API.xml"));


                c.OperationFilter<SwaggerDefaultValues>();

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "Authorization header using the Bearer scheme. Example: {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);
                var securityRequirement = new OpenApiSecurityRequirement();
                securityRequirement.Add(securitySchema, new[] { "Bearer" });
                c.AddSecurityRequirement(securityRequirement);

                c.DescribeAllParametersInCamelCase();
            });
            services.AddResponseCompression();
            services.AddControllers().SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            var appSettings = Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();


            var key = Encoding.ASCII.GetBytes(appSettings.ConnectJwt.ApiSecret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        DateTime secretDate = DateTime.UtcNow.AddSeconds(appSettings.ConnectJwt.AccessTokenLongExpireTime);

                        if (DateTime.UtcNow.Subtract(secretDate).TotalSeconds > 0)
                        {
                            context.Response.Headers.Add("token-expired", "true");
                        }

                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "connect.api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(ConnectConstants.CORSS_POLICY_NAME);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/connectPlanetChats");
            });
        }
    }
}
