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
    public class DContractorsController : ControllerBase
    {
        private readonly IUnitOfWork _context;

        public DContractorsController(IUnitOfWork context)
        {
            _context = context;
        }

        // GET: api/DContractors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DContractor>>> GetDContractors()
        {
            if (_context.DContractorRepository == null)
            {
                return NotFound();
            }
            int? companyId = await GetCompanyId();
            var result = await _context.DContractorRepository.GetAsync(x=>x.CompanyId == companyId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET: api/DContractors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DContractor>> GetDContractor(int id)
        {
            if (_context.DContractorRepository == null)
            {
                return NotFound();
            }
            int? companyId = await GetCompanyId();
            var dContractor = (await _context.DContractorRepository.GetAsync(x=>x.Id == id && x.CompanyId == companyId)).FirstOrDefault();

            if (dContractor == null)
            {
                return NotFound();
            }

            return dContractor;
        }

        // PUT: api/DContractors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDContractor(int id, DContractor dContractor)
        {
            if (id != dContractor.Id)
            {
                return BadRequest();
            }

            _context.DContractorRepository.Update(dContractor);

            try
            {
                await _context.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DContractorExists(id))
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

        // POST: api/DContractors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DContractor>> PostDContractor(DContractor dContractor)
        {
            if (_context.DContractorRepository == null)
            {
                return Problem("Entity set 'DContractors'  is null.");
            }
            _context.DContractorRepository.Insert(dContractor);
            await _context.SaveAsync();

            return CreatedAtAction("GetDContractor", new { id = dContractor.Id }, dContractor);
        }

        // DELETE: api/DContractors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDContractor(int id)
        {
            if (_context.DContractorRepository == null)
            {
                return NotFound();
            }
            var dContractor = await _context.DContractorRepository.GetByIdAsync(id);
            if (dContractor == null)
            {
                return NotFound();
            }

            _context.DContractorRepository.Delete(dContractor);
            await _context.SaveAsync();

            return NoContent();
        }

        private bool DContractorExists(int id)
        {
            return _context.DContractorRepository.Exist(id);
        }

        private async Task<int?> GetCompanyId()
        {
            var userName = User?.Identity?.Name;
            var id = (await _context.UserRegisteredRepository.GetAsync(x => x.Name == userName)).FirstOrDefault()?.CompanyId;

            return id;
        }
    }
}
