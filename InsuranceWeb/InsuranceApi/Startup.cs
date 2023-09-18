using AutoMapper;
using InsuranceApi.Extensions;
using InsuranceApi.Mapping;
using InsuranceCore;
using InsuranceCore.Interface;
using InsuranceCore.Models;
using InsuranceCore.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
using System.Threading.Tasks;

namespace InsuranceApi
{
    public class Startup
    {
      
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionStrings = Configuration.GetConnectionString("Default");
        }

        public IConfiguration Configuration { get; }
        public string ConnectionStrings { get; set; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));
            var jwtSettings = Configuration.GetSection("Jwt").Get<JwtSettings>();
            services.AddControllers();
            services.AddDbContext<InsuranceContext>(options => options.UseSqlServer(ConnectionStrings));

            services.AddTransient<IClaimCenterRepository, ClaimCenterRepository>();
            services.AddTransient<ILookupsRepository, LookupsRepository>();
            services.AddTransient<IAuthRepository, AuthRepository>();
            services.AddTransient<IHospitalRepository, HospitalRepository>();
            services.AddTransient<IInsuredRepository, InsuredRepository>();
            services.AddTransient<ITreatingRepository, TreatingRepository>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JwtAuthentication:JwtIssuer"],
                    ValidAudience = Configuration["JwtAuthentication:JwtIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtAuthentication:JwtKey"])),
                };

            });



            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "InsuranceApi", Version = "v1" });


                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT containing userid claim",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });

                var security =
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                },
                                UnresolvedReference = true
                            },
                            new List<string>()
                        }
                    };
                c.AddSecurityRequirement(security);




            });
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                      .SetPreflightMaxAge(TimeSpan.FromDays(3600))
                       ;


            }));
            services.AddControllersWithViews()
              .AddNewtonsoftJson(options =>
              options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
       );

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddAuth(jwtSettings);



        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
              
            }
            else
            {
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InsuranceApi v1"));
            // app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuth();
           // app.UseAuthorization();
            app.UseCors("MyPolicy");
          // app.UseAuthentication();

            //    app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
