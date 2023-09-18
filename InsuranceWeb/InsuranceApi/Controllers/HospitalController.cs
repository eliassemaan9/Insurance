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
    [Route("api/Hospital")]
    [ApiController]
   
    public class HospitalController : ControllerBase
    {

        private readonly IHospitalRepository hospitalRepository;
        public HospitalController(IHospitalRepository hospitalRepository)
        {
            this.hospitalRepository = hospitalRepository;

        }
      
        [HttpGet]
        [Route("GetHospitals")]

        public async Task<ActionResult<IEnumerable<Hospital>>> GetHospitals()
        {
            var result = hospitalRepository.GetHospitals();
            return Ok(result.Result);
        }
    }
}
