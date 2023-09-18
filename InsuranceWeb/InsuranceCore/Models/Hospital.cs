using System;
using System.Collections.Generic;

#nullable disable

namespace InsuranceCore.Models
{
    public partial class Hospital
    {
        public Hospital()
        {
            ClaimCenters = new HashSet<ClaimCenter>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Other { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<ClaimCenter> ClaimCenters { get; set; }
    }
}
