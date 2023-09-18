using InsuranceCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceCore.Interface
{
    public interface IAuthRepository
    {
        bool signIn(string email, string password);
        Task<User> GetUserByEmail(string email);
    }
}
