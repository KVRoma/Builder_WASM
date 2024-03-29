﻿using System;
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
using System.Text.Json;

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
        // GET: api/UserMessages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserMessage>>> GetUserMessages()
        {
            if (_context.UserMessageRepository == null)
            {
                return NotFound(new {message = "Repository not found!"});
            }
            var result = await _context.UserMessageRepository.GetAsync(x => !string.IsNullOrWhiteSpace(x.Message));            
            return Ok(result);
        }

        // GET: api/UserMessages/user/5
        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<UserMessage>>> GetUserMessages(int id)
        {
          if (_context.UserMessageRepository == null)
          {
              return NotFound(new {message = "Repository not found!"});
          }
            var result = await _context.UserMessageRepository.GetAsync(x=>x.UserRegisteredId == id && !string.IsNullOrWhiteSpace(x.Message));
            return Ok(result);
        }

        // GET: api/UserMessages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserMessage>> GetUserMessage(int id)
        {
          if (_context.UserMessageRepository == null)
          {
              return NotFound(new {message = "Repository not found!"});
          }
            var userMessage = await _context.UserMessageRepository.GetByIdAsync(id);

            if (userMessage == null)
            {
                return NotFound(new {message = "Item not found!"});
            }

            return Ok(userMessage);
        }

        // PUT: api/UserMessages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutUserMessage(int id, UserMessage userMessage)
        {
            if (id != userMessage.Id)
            {
                return BadRequest(new {message = "Item not found!"});
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
                    return NotFound(new {message = "Item not found!"});
                }
                else
                {
                    return BadRequest(new { message = "Error <Put>. Try later..." });
                }
            }

            return Ok(new { message = "Your action is successful" });
        }

        // POST: api/UserMessages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserMessage>> PostUserMessage(UserMessage userMessage)
        {
          if (_context.UserMessageRepository == null)
          {
              return NotFound(new {message = "Repository not found!"});
          }
            _context.UserMessageRepository.Insert(userMessage);
            await _context.SaveAsync();

            return CreatedAtAction(nameof(GetUserMessage), new { id = userMessage.Id }, userMessage);
        }

        // DELETE: api/UserMessages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserMessage(int id)
        {
            if (_context.UserMessageRepository == null)
            {
                return NotFound(new {message = "Repository Not found!"});
            }
            var userMessage = await _context.UserMessageRepository.GetByIdAsync(id);
            if (userMessage == null)
            {
                return NotFound(new {message = "Item not found!"});
            }

            _context.UserMessageRepository.Delete(userMessage);
            await _context.SaveAsync();

            return Ok(new { message = "Your action is successful" });
        }




        private bool UserMessageExists(int id)
        {
            return _context.UserMessageRepository.Exist(id);
        }
    }
}
