using System;
using System.Collections.Generic;

#nullable disable

namespace InsuranceCore.Models
{
    public partial class ClaimCenter
    {
        public int Id { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public string MedicalCase { get; set; }
        public decimal? EstimatedCost { get; set; }
        public string Remarks { get; set; }
        public int? InsuredId { get; set; }
        public int? HospitalId { get; set; }
        public int? TreatingId { get; set; }
        public int? StatusId { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Hospital Hospital { get; set; }
        public virtual Insured Insured { get; set; }
        public virtual Lookup Status { get; set; }
        public virtual TreatingPhysician Treating { get; set; }
    }
}
