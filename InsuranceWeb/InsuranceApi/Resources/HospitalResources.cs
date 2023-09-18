using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceApi.Resources
{
    public class HospitalResources
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Other { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
