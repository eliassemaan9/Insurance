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
    [Route("api/Insured")]
    [ApiController]
    public class InsuredController : ControllerBase
    {
        private readonly IInsuredRepository insuredRepository;
        public InsuredController(IInsuredRepository insuredRepository)
        {
            this.insuredRepository = insuredRepository;

        }

        [HttpGet]
        [Route("GetInsured")]

        public async Task<ActionResult<IEnumerable<Insured>>> GetInsured()
        {
            var result = insuredRepository.getInsured();
            return Ok(result.Result);
        }
        [HttpGet("GetInsuredByCardNumber")]
        public async Task<ActionResult<IEnumerable<Insured>>> GetInsuredByCardNumber(string CardNumber)
        {
            var result = await insuredRepository.GetInsuredByCardNumber(CardNumber);

            return Ok(result);
        }
    }
}
