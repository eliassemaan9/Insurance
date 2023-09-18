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
    public class TreatingRepository : ITreatingRepository
    {
        private readonly IConfiguration configuration;

        private readonly InsuranceContext MyDbContext;
        public TreatingRepository(InsuranceContext context, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.MyDbContext = context;

        }
        public async Task<IEnumerable<TreatingPhysician>> getTreatingPhysician()
        {

            return await MyDbContext.TreatingPhysicians.ToListAsync();
        }

    }
}
