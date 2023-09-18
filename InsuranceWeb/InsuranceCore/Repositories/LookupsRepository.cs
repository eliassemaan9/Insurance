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
    public class LookupsRepository : ILookupsRepository
    {
        private readonly IConfiguration configuration;

        private readonly InsuranceContext MyDbContext;
        public LookupsRepository(InsuranceContext context, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.MyDbContext = context;

        }
        public async Task<IEnumerable<Lookup>> GetLookupsByParantCode(string code)
        {
            var parentLookup = await MyDbContext.Lookups.SingleOrDefaultAsync(m => m.LookupCode == code);
            return await MyDbContext.Lookups.Where(m => m.ParentId == parentLookup.Id).ToListAsync();
        }
       
        public async Task<Lookup> GetLookupByCode(string code)
        {
            return await MyDbContext.Lookups.Where(m => m.LookupCode == code).SingleOrDefaultAsync();
        }

        public async Task<Lookup> GetLookupById(int id)
        {
            return await MyDbContext.Lookups.Where(m => m.Id == id).SingleOrDefaultAsync();
        }

    }
}
