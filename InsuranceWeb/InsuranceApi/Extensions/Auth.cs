using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceApi.Extensions
{
    public static class Auth
    {

        public static IServiceCollection AddAuth(
            this IServiceCollection services,
            JwtSettings jwtSettings)
        {
            services
                .AddAuthorization(options =>
                {
                    options.AddPolicy("OnlyTest", policy => policy.RequireUserName("test@test.com"));
                })
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                });
            //.AddJwtBearer(options =>
            //{
            //    options.RequireHttpsMetadata = false;
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidIssuer = jwtSettings.Issuer,
            //        ValidAudience = jwtSettings.Issuer,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
            //        ClockSkew = TimeSpan.Zero
            //    };
            //});

            return services;
        }

        public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
        {
            app.UseAuthentication();

            app.UseAuthorization();

            return app;
        }

    }
}
