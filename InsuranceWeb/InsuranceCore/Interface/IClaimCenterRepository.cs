using InsuranceCore.Models;
using InsuranceCore.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceCore.Interface
{
    public interface IClaimCenterRepository
    {
        Task<IEnumerable<ClaimCenterView>> GetAllClaimCenterData(DateTime? fromDate, DateTime? toDate, string cardNumber, int? hospitalId);
        Task<Tuple<bool, string>> SaveClaimCenter(int id,string remarks, string medicalCase, DateTime? admissionDate, decimal? estimatedCost, int? insuredId
           , int? hospitalId, int? treatingId, int? statusId, bool? isDeleted);

        Task<ClaimCenter> GetClaimCenterById(int id);
    }
}
