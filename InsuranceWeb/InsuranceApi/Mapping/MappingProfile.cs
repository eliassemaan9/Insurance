using AutoMapper;
using InsuranceApi.Resources;
using InsuranceCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceApi.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Lookup, LookupResources>();
        }
    
    }
}
