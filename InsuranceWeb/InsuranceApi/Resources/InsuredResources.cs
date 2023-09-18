using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceApi.Resources
{
    public class InsuredResources
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public DateTime? Dob { get; set; }
        public int? GenderId { get; set; }
    }
}
