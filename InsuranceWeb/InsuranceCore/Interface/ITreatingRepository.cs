using InsuranceCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceCore.Interface
{
    public interface ITreatingRepository
    {
        Task<IEnumerable<TreatingPhysician>> getTreatingPhysician();
    }
}
