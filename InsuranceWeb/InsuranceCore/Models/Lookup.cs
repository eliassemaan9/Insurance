using System;
using System.Collections.Generic;

#nullable disable

namespace InsuranceCore.Models
{
    public partial class Lookup
    {
        public Lookup()
        {
            ClaimCenters = new HashSet<ClaimCenter>();
            Insureds = new HashSet<Insured>();
        }

        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string LookupCode { get; set; }
        public string Name { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual ICollection<ClaimCenter> ClaimCenters { get; set; }
        public virtual ICollection<Insured> Insureds { get; set; }
    }
}
