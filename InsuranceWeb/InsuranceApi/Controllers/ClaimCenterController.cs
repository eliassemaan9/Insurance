using InsuranceApi.Resources;
using InsuranceCore.Interface;
using InsuranceCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceApi.Controllers
{
    [Authorize]
    [Route("api/ClaimCenter")]
    [ApiController]
    public class ClaimCenterController : ControllerBase
    {
        private readonly IClaimCenterRepository IclaimCenterRepository;
        public ClaimCenterController(IClaimCenterRepository iclaimCenterRepository)
        {
            this.IclaimCenterRepository = iclaimCenterRepository;

        }

        [HttpGet]
        [Route("GetClaimCenterData")]

        public async Task<IActionResult> GetClaimCenterData(DateTime? fromDate, DateTime? toDate, string cardNumber, int? hospitalId)
        {
         
            var result = IclaimCenterRepository.GetAllClaimCenterData(fromDate,toDate,cardNumber,hospitalId);
            return Ok(result.Result); 
        }
        [HttpPost("SaveClaimCenter")]
        public async Task<IActionResult> SaveClaimCenter(ClaimCenterResources claimCenterResources)
        {
            var result = await IclaimCenterRepository.SaveClaimCenter(claimCenterResources.Id, claimCenterResources.Remarks, claimCenterResources.MedicalCase,claimCenterResources.AdmissionDate, claimCenterResources.EstimatedCost, 
                claimCenterResources.InsuredId , claimCenterResources.HospitalId, claimCenterResources.TreatingId, claimCenterResources.StatusId, claimCenterResources.IsDeleted);

          
            if (result.Item1)
            {
                return Ok(new
                {
                    success = true,
                    Message = "Claim Center Saved Succesfully"
                });
            }
            return Ok(new
            {
                success = false,
                Message = "Faild to save Claim Center"
            });
        }

        [HttpGet("GetClaimCenterById")]
        public async Task<ActionResult<IEnumerable<ClaimCenter>>> GetClaimCenterById(int id)
        {
            var result = await IclaimCenterRepository.GetClaimCenterById(id);

            return Ok(result);
        }


    }
}
