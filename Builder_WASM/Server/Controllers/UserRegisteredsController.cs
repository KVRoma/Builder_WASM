using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Builder_WASM.Server.Data;
using Builder_WASM.Shared.Entities;
using Builder_WASM.Server.Services;
using Microsoft.AspNetCore.Authorization;

namespace Builder_WASM.Server.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegisteredsController : ControllerBase
    {
        private readonly IUnitOfWork _context;

        public UserRegisteredsController(IUnitOfWork context)
        {
            _context = context;
        }

        // GET: api/UserRegistereds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRegistered>>> GetUserRegistereds()
        {
          if (_context.UserRegisteredRepository == null)
          {
              return NotFound();
          }
          var result = await _context.UserRegisteredRepository.GetAsync(x => x.Role != "Admin");
          return Ok(result);
        }

        // GET: api/UserRegistereds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserRegistered>> GetUserRegistered(int id)
        {
          if (_context.UserRegisteredRepository == null)
          {
              return NotFound();
          }
            var userRegistered = await _context.UserRegisteredRepository.GetByIdAsync(id);

            if (userRegistered == null)
            {
                return NotFound();
            }

            return userRegistered;
        }

        // PUT: api/UserRegistereds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserRegistered(int id, UserRegistered userRegistered)
        {
            if (id != userRegistered.Id)
            {
                return BadRequest();
            }

            _context.UserRegisteredRepository.Update(userRegistered);

            try
            {
                await _context.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {                
                throw;                
            }

            return NoContent();
        }

        // POST: api/UserRegistereds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserRegistered>> PostUserRegistered(UserRegistered userRegistered)
        {
          if (_context.UserRegisteredRepository == null)
          {
              return Problem("Entity set 'UserRegistereds'  is null.");
          }
            _context.UserRegisteredRepository.Insert(userRegistered);
            await _context.SaveAsync();

            return CreatedAtAction("GetUserRegistered", new { id = userRegistered.Id }, userRegistered);
        }

        // DELETE: api/UserRegistereds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserRegistered(int id)
        {
            if (_context.UserRegisteredRepository == null)
            {
                return NotFound();
            }
            var userRegistered = await _context.UserRegisteredRepository.GetByIdAsync(id);
            if (userRegistered == null)
            {
                return NotFound();
            }

            _context.UserRegisteredRepository.Delete(userRegistered);
            await _context.SaveAsync();

            return NoContent();
        }

        private bool UserRegisteredExists(int id)
        {
            return _context.UserRegisteredRepository.Exist(id);
        }
    }
}
