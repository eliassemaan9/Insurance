using InsuranceApi.Resources;
using InsuranceCore.Interface;
using InsuranceCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApi.Controllers
{
    
    [ApiController]
    [Route("api/auth")]
    public class AuthController :ControllerBase
    {
        private readonly IAuthRepository authRepository;
        private readonly IConfiguration  Configuration;
        public AuthController(IAuthRepository authRepository, IConfiguration Configuration)
        {
            this.authRepository = authRepository;
            this.Configuration = Configuration;
        }


        [HttpPost("SignIn")]

        public async Task<IActionResult> SignIn(UserResources userResource)
        {

            bool istrue = authRepository.signIn(userResource.Email, userResource.PasswordHash);
            User user = new User();
            if (istrue == false )
            {
                return Ok(new
                {
                    success = false,
                    Message = "Wrong Username or password"
                });
            }

            if (user.IsActive == false)
            {
                return Ok(new
                {
                    success = false,
                    Message = "This account is Deactivated, please contact the administrator"
                });
            }

            //  var userSigninResult = await _signInManager.PasswordSignInAsync(user, userLoginResource.Password, false, true);

            if (istrue == true)
            {
                user = await authRepository.GetUserByEmail(userResource.Email);



                return Ok(new
                {
                    success = true,
                    token = GenerateJwt(user)
                });
            }
            if (user.IsDeleted == true)
            {
                return Ok(new
                {
                    success = false,
                    Message = "This Account is deleted"
                });
            }

            return Ok(new
            {
                success = false,
                Message = "Wrong Username or password"
            });
        }

        private string GenerateJwt(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim("UserName", user.UserName),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
          
                new Claim("exp2", DateTime.Now.AddHours(12).ToString() )



            //new Claim("Jti", Guid.NewGuid().ToString()),
        };

        


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtAuthentication:JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpirationInDays));

            var token = new JwtSecurityToken(
                issuer: Configuration["JwtAuthentication:JwtIssuer"],
                audience: Configuration["JwtAuthentication:JwtAudience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
