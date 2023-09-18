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
    public class HospitalRepository : IHospitalRepository
    {
        private readonly IConfiguration configuration;

        private readonly InsuranceContext MyDbContext;
        public HospitalRepository(InsuranceContext context, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.MyDbContext = context;

        }
        public async Task<IEnumerable<Hospital>> GetHospitals()
        {
            
            return await MyDbContext.Hospitals.ToListAsync();
        }
    }
}
