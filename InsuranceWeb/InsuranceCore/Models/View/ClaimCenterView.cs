using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceCore.Models.View
{
    public class ClaimCenterView
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
        public string CardNumber { get; set; }
        public string Status { get; set; }
        public string InsuredName { get; set; }
        public string TreatingName { get; set; }
        public string Hospital { get; set; }

    }
}
