using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleUserRepo.Context;
using SampleUserRepo.Interfaces;
using SampleUserRepo.Models;
using SampleUserRepo.Models.Services;

namespace SampleUserRepo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryPreferencesController : ControllerBase
    {
        private readonly ICountryPreferenceService countryPreferenceService;

        public CountryPreferencesController(ICountryPreferenceService countryPreferenceService)
        {
            this.countryPreferenceService = countryPreferenceService;
        }

        // GET: api/CountryPreferences
        [HttpGet]
        public async Task<ActionResult<CountryPreferenceResponse>> GetCountryPreference()
        {
            return await countryPreferenceService.GetCountryPreference("*");
        }

    }
}
