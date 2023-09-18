using InsuranceCore.Interface;
using InsuranceCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceCore.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration configuration;

        private readonly InsuranceContext Icontext;
        public AuthRepository(InsuranceContext context, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.Icontext = context;

        }
        public bool signIn(string email,string password)
        {
            var User = Icontext.Users.Where(m => m.Email == email && m.PasswordHash == password).Count();
            if (User == 1)
                return true;
            else
                return false;
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await Icontext.Users.Where(m => m.Email == email).SingleOrDefaultAsync();
        }
    }
}
