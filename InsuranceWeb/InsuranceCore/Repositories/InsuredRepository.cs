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
    public class InsuredRepository : IInsuredRepository
    {
        private readonly IConfiguration configuration;

        private readonly InsuranceContext MyDbContext;
        public InsuredRepository(InsuranceContext context, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.MyDbContext = context;

        }
        public async Task<IEnumerable<Insured>> getInsured()
        {

            return await MyDbContext.Insureds.ToListAsync();
        }

        public async Task<Insured> GetInsuredByCardNumber(string cardNumber)
        {
            return await MyDbContext.Insureds.Include(m => m.Gender).Where(m => m.CardNumber == cardNumber).SingleOrDefaultAsync();
        }
    }
}
