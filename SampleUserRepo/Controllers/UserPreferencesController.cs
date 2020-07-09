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
    public class UserPreferencesController : ControllerBase
    {
        private readonly crsuserauthdeContext _context;

        public UserPreferencesController(crsuserauthdeContext context)
        {
            _context = context;
        }

        // GET: api/UserPreferences
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPreferences>>> GetUserPreferences()
        {
            return await _context.UserPreferences.ToListAsync();
        }

        // GET: api/UserPreferences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserPreferences>> GetUserPreferences(Guid id)
        {
            var userPreferences = await _context.UserPreferences.FindAsync(id);

            if (userPreferences == null)
            {
                return NotFound();
            }

            return userPreferences;
        }

        // PUT: api/UserPreferences/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserPreferences(Guid id, UserPreferences userPreferences)
        {
            if (id != userPreferences.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userPreferences).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPreferencesExists(id))
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


        /*// DELETE: api/UserPreferences/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserPreferences>> DeleteUserPreferences(Guid id)
        {
            var userPreferences = await _context.UserPreferences.FindAsync(id);
            if (userPreferences == null)
            {
                return NotFound();
            }

            _context.UserPreferences.Remove(userPreferences);
            await _context.SaveChangesAsync();

            return userPreferences;
        }*/

        private bool UserPreferencesExists(Guid id)
        {
            return _context.UserPreferences.Any(e => e.UserId == id);
        }
    }
}
