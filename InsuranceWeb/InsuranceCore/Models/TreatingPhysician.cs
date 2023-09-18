using System;
using System.Collections.Generic;

#nullable disable

namespace InsuranceCore.Models
{
    public partial class TreatingPhysician
    {
        public TreatingPhysician()
        {
            ClaimCenters = new HashSet<ClaimCenter>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public int? HospitalId { get; set; }

        public virtual ICollection<ClaimCenter> ClaimCenters { get; set; }
    }
}
