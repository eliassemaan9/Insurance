using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceApi.Resources
{
    public class LookupResources
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string LookupCode { get; set; }
        public string Name { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
