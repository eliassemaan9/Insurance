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
    [Route("api/Treating")]
    [ApiController]
    public class TreatingController : ControllerBase
    {

        private readonly ITreatingRepository treatingRepository;
        public TreatingController(ITreatingRepository treatingRepository)
        {
            this.treatingRepository = treatingRepository;

        }

        [HttpGet]
        [Route("GetTreating")]

        public async Task<ActionResult<IEnumerable<TreatingPhysician>>> getTreating()
        {
            var result = treatingRepository.getTreatingPhysician();
            return Ok(result.Result);
        }
    }
}
