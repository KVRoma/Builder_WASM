using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Builder_WASM.Server.Data;
using Builder_WASM.Shared.Entities.Dictionary;
using Microsoft.AspNetCore.Authorization;
using Builder_WASM.Server.Services;

namespace Builder_WASM.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DGroupesController : ControllerBase
    {
        private readonly IUnitOfWork _context;

        public DGroupesController(IUnitOfWork context)
        {
            _context = context;
        }

        // GET: api/DGroupes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DGroupe>>> GetDGroupes()
        {
            if (_context.DGroupeRepository == null)
            {
                return NotFound(new { message = "Repository not found" });
            }
            int? companyId = await GetCompanyId();
            var result = await _context.DGroupeRepository.GetAsync(x => x.CompanyId == companyId);
            if (result == null)
            {
                return NotFound(new { message = "Item not found" });
            }
            return Ok(result);
        }

        // GET: api/DGroupes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DGroupe>> GetDGroupe(int id)
        {
            if (_context.DGroupeRepository == null)
            {
                return NotFound(new { message = "Repository not found" });
            }
            int? companyId = await GetCompanyId();
            var dGroupe = (await _context.DGroupeRepository.GetAsync(x=>x.Id == id && x.CompanyId == companyId)).FirstOrDefault();

            if (dGroupe == null)
            {
                return NotFound(new { message = "Item not found" });
            }

            return Ok(dGroupe);
        }

        // PUT: api/DGroupes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDGroupe(int id, DGroupe dGroupe)
        {
            if (id != dGroupe.Id)
            {
                return BadRequest(new { message = "Item not found" });
            }

            _context.DGroupeRepository.Update(dGroupe);

            try
            {
                await _context.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DGroupeExists(id))
                {
                    return NotFound(new { message = "Item not found" });
                }
                else
                {
                    return BadRequest(new { message = "Error <Put>. Try later..." });
                }
            }

            return Ok(new { message = "Your action is successful" });
        }

        // POST: api/DGroupes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DGroupe>> PostDGroupe(DGroupe dGroupe)
        {
            if (_context.DGroupeRepository == null)
            {
                return NotFound(new { message = "Repository not found" });
            }

            dGroupe.CompanyId = await GetCompanyId();
            if (dGroupe.CompanyId == 0)
            {
                return BadRequest(new { message = "You are not registered with any company!" });
            }
            _context.DGroupeRepository.Insert(dGroupe);
            await _context.SaveAsync();

            return CreatedAtAction(nameof(GetDGroupe), new { id = dGroupe.Id }, dGroupe);
        }

        // DELETE: api/DGroupes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDGroupe(int id)
        {
            if (_context.DGroupeRepository == null)
            {
                return NotFound(new { message = "Repository not found" });
            }
            var dGroupe = await _context.DGroupeRepository.GetByIdAsync(id);
            if (dGroupe == null)
            {
                return NotFound(new { message = "Item not found" });
            }

            _context.DGroupeRepository.Delete(dGroupe);
            await _context.SaveAsync();

            return Ok(new { message = "Your action is successful" });
        }




        private bool DGroupeExists(int id)
        {
            return _context.DGroupeRepository.Exist(id);
        }

        private async Task<int> GetCompanyId()
        {
            var userName = User?.Identity?.Name;
            var id = (await _context.UserRegisteredRepository.GetAsync(x => x.Name == userName)).FirstOrDefault()?.CompanyId ?? 0;

            return id;
        }
    }
}
