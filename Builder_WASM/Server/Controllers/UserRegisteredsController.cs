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
using Builder_WASM.Shared.Models;
using System.Text.Json;

namespace Builder_WASM.Server.Controllers
{
    [Authorize]
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
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRegistered>>> GetUserRegistereds()
        {
            if (_context.UserRegisteredRepository == null)
            {
                return NotFound(new { message="Repository not found!"});
            }
            var result = await _context.UserRegisteredRepository.GetAsync(x => x.Role != "Admin",includeProperties: "Messages,Company");
            
            return Ok(result);
        }

        // GET: api/UserRegistereds/5
        [Authorize(Roles = "Admin, User")]        
        [HttpGet("{id}")]
        public async Task<ActionResult<UserRegistered>> GetUserRegistered(int id)
        {
            if (_context.UserRegisteredRepository == null)
            {
                return NotFound(new { message = "Repository not found!" });
            }
            var userRegistered = (await _context.UserRegisteredRepository.GetAsync(x=>x.Id == id, includeProperties: "Messages,Company")).FirstOrDefault();

            if (userRegistered == null)
            {
                return NotFound(new { message = "User not found!" });
            }

            return Ok(userRegistered);
        }

        // PUT: api/UserRegistereds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> PutUserRegistered(int id, UserRegistered userRegistered)
        {            
            if (id != userRegistered?.Id)
            {
                return BadRequest(new { message = "Repository not found!" });
            }
            var user = (await _context.UserRegisteredRepository.GetAsync(x=>x.Id == id, includeProperties: "Messages")).FirstOrDefault();
            if(user == null)
            { 
                return BadRequest(new { message = "This user is not in the database!" }); 
            }
            
            user.CopyWithoutPassword(userRegistered);

            _context.UserRegisteredRepository.Update(user);

            try
            {
                await _context.SaveAsync();
            }
            catch (Exception ex)
            {
                if (!UserRegisteredExists(id))
                {
                    return NotFound(new { message = "User not found!" });
                }
                else
                {
                    return BadRequest(new { message = "Error <Put>. Try later..." });
                }
            }

            return Ok(new { message= "Your action is successful!" });
        }

        // POST: api/UserRegistereds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserRegistered>> PostUserRegistered(AuthenticateRequestSignup response)
        {
            if (_context.UserRegisteredRepository == null)
            {
                return NotFound(new { message = "Repository not found!"});
            }

            var user = (await _context.UserRegisteredRepository.GetAsync(x=>x.Name == response.Username)).FirstOrDefault();
            if (user != null)
            {
                return BadRequest(new {message = "This username already exists!" } );
            }


            user = new UserRegistered()
            {
                Name = response.Username,
                Password = response.Password,
                Role = "User"
            };


            if (!string.IsNullOrWhiteSpace(response.MessageNewUser))
            {
                UserMessage mess = new UserMessage()
                {
                    Message = "User: " + response.MessageNewUser
                };

                user.Messages.Add(mess);
            }

            _context.UserRegisteredRepository.Insert(user);
            await _context.SaveAsync();

           
            return CreatedAtAction(nameof(GetUserRegistered), new { id = user.Id }, user);
        }

        // DELETE: api/UserRegistereds/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserRegistered(int id)
        {
            if (_context.UserRegisteredRepository == null)
            {
                return NotFound(new { message = "Repository not found!"});
            }
            var userRegistered = await _context.UserRegisteredRepository.GetByIdAsync(id);
            if (userRegistered == null)
            {
                return NotFound(new {message="User not found!"});
            }

            _context.UserRegisteredRepository.Delete(userRegistered);
            await _context.SaveAsync();

            return Ok(new { message= "Your action is successful" });
        }




        private bool UserRegisteredExists(int id)
        {
            return _context.UserRegisteredRepository.Exist(id);
        }

    }
}
