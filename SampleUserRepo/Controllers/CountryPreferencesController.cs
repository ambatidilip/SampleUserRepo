using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleUserRepo.Context;
using SampleUserRepo.Models;

namespace SampleUserRepo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryPreferencesController : ControllerBase
    {
        private readonly crsuserauthdeContext _context;

        public CountryPreferencesController(crsuserauthdeContext context)
        {
            _context = context;
        }

        // GET: api/CountryPreferences
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryPreference>>> GetCountryPreference()
        {
            return await _context.CountryPreference.ToListAsync();
        }

        // GET: api/CountryPreferences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryPreference>> GetCountryPreference(Guid id)
        {
            var countryPreference = await _context.CountryPreference.FindAsync(id);

            if (countryPreference == null)
            {
                return NotFound();
            }

            return countryPreference;
        }

        // PUT: api/CountryPreferences/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountryPreference(Guid id, CountryPreference countryPreference)
        {
            if (id != countryPreference.Id)
            {
                return BadRequest();
            }

            _context.Entry(countryPreference).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryPreferenceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CountryPreferences
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CountryPreference>> PostCountryPreference(CountryPreference countryPreference)
        {
            _context.CountryPreference.Add(countryPreference);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CountryPreferenceExists(countryPreference.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCountryPreference", new { id = countryPreference.Id }, countryPreference);
        }

        // DELETE: api/CountryPreferences/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CountryPreference>> DeleteCountryPreference(Guid id)
        {
            var countryPreference = await _context.CountryPreference.FindAsync(id);
            if (countryPreference == null)
            {
                return NotFound();
            }

            _context.CountryPreference.Remove(countryPreference);
            await _context.SaveChangesAsync();

            return countryPreference;
        }

        private bool CountryPreferenceExists(Guid id)
        {
            return _context.CountryPreference.Any(e => e.Id == id);
        }
    }
}
