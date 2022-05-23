using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Builder_WASM.Server.Data;
using Builder_WASM.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Builder_WASM.Server.Services;

namespace Builder_WASM.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserMessagesController : ControllerBase
    {
        private readonly IUnitOfWork _context;

        public UserMessagesController(IUnitOfWork context)
        {
            _context = context;
        }

        // GET: api/UserMessages/user/5
        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<UserMessage>>> GetUserMessages(int id)
        {
          if (_context.UserMessageRepository == null)
          {
              return NotFound();
          }
            var result = await _context.UserMessageRepository.GetAsync(x=>x.UserRegisteredId == id);
            return Ok(result);
        }

        // GET: api/UserMessages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserMessage>> GetUserMessage(int id)
        {
          if (_context.UserMessageRepository == null)
          {
              return NotFound();
          }
            var userMessage = await _context.UserMessageRepository.GetByIdAsync(id);

            if (userMessage == null)
            {
                return NotFound();
            }

            return userMessage;
        }

        // PUT: api/UserMessages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserMessage(int id, UserMessage userMessage)
        {
            if (id != userMessage.Id)
            {
                return BadRequest();
            }

            _context.UserMessageRepository.Update(userMessage);

            try
            {
                await _context.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserMessageExists(id))
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

        // POST: api/UserMessages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserMessage>> PostUserMessage(UserMessage userMessage)
        {
          if (_context.UserMessageRepository == null)
          {
              return Problem("Entity set 'UserMessages'  is null.");
          }
            _context.UserMessageRepository.Insert(userMessage);
            await _context.SaveAsync();

            return CreatedAtAction("GetUserMessage", new { id = userMessage.Id }, userMessage);
        }

        // DELETE: api/UserMessages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserMessage(int id)
        {
            if (_context.UserMessageRepository == null)
            {
                return NotFound();
            }
            var userMessage = await _context.UserMessageRepository.GetByIdAsync(id);
            if (userMessage == null)
            {
                return NotFound();
            }

            _context.UserMessageRepository.Delete(userMessage);
            await _context.SaveAsync();

            return NoContent();
        }

        private bool UserMessageExists(int id)
        {
            return _context.UserMessageRepository.Exist(id);
        }
    }
}
