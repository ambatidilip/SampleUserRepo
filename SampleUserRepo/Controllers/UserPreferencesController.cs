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
using SampleUserRepo.Resources;

namespace SampleUserRepo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPreferencesController : ControllerBase
    {
        private readonly IUserPreferenceService userPreferenceService;

        public UserPreferencesController(IUserPreferenceService userPreferenceService)
        {
            this.userPreferenceService = userPreferenceService;
        }

        // GET: api/CountryPreferences
        [HttpGet("{id}")]
        public async Task<ActionResult<UserPreferenceResponse>> Get(string id)
        {
            return await userPreferenceService.GetAsync(id);
        }

        // GET: api/CountryPreferences
        [HttpPut]
        public async Task<ActionResult<UserPreferenceResponse>> Put(UserPreferenceResource resource)
        {
            return await userPreferenceService.PutAsync(resource);
        }
    }
}
