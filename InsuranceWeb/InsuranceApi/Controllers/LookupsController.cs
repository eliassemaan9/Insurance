using AutoMapper;
using InsuranceApi.Resources;
using InsuranceCore.Interface;
using InsuranceCore.Models;
using InsuranceCore.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceApi.Controllers
{
    [Authorize]
    [Route("api/Lookups")]
    [ApiController]
    public class LookupsController : ControllerBase
    {
        private readonly ILookupsRepository IlookupsRepository;
        private readonly IMapper mapper;
        public LookupsController(ILookupsRepository IlookupsRepository, IMapper mapper)
        {
            this.IlookupsRepository = IlookupsRepository;
            this.mapper = mapper;

        }
      
        [HttpGet("GetLookupByParentCode")]
        public async Task<ActionResult<IEnumerable<LookupResources>>> GetLookupByParantCode(string code)
        {
            var result = await IlookupsRepository.GetLookupsByParantCode(code);
            var lookups = mapper.Map<IEnumerable<Lookup>, IEnumerable<LookupResources>>(result);

            return Ok(lookups);
        }

    }
}
