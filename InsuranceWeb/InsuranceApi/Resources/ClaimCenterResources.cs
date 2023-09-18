using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceApi.Resources
{
    public class ClaimCenterResources
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
    }
}
