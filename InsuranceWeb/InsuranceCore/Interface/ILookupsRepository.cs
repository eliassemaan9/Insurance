using InsuranceCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceCore.Interface
{
    public interface ILookupsRepository
    {
        Task<IEnumerable<Lookup>> GetLookupsByParantCode(string code);
        Task<Lookup> GetLookupByCode(string code);
        Task<Lookup> GetLookupById(int id);

    }
}
