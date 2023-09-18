using System;
using System.Collections.Generic;

#nullable disable

namespace InsuranceCore.Models
{
    public partial class Insured
    {
        public Insured()
        {
            ClaimCenters = new HashSet<ClaimCenter>();
        }

        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public DateTime? Dob { get; set; }
        public int? GenderId { get; set; }

        public virtual Lookup Gender { get; set; }
        public virtual ICollection<ClaimCenter> ClaimCenters { get; set; }
    }
}
