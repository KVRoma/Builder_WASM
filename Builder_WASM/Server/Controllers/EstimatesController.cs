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
    public class EstimatesController : ControllerBase
    {
        private readonly IUnitOfWork _context;

        public EstimatesController(IUnitOfWork context)
        {
            _context = context;
        }

        // GET: api/Estimates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estimate>>> GetEstimates()
        {
            if (_context.EstimateRepository == null)
            {
                return NotFound();
            }
            int? id = await GetClientId();
            var result = await _context.EstimateRepository.GetAsync(x=>x.ClientJobId == id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET: api/Estimates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estimate>> GetEstimate(int id)
        {
            if (_context.EstimateRepository == null)
            {
                return NotFound();
            }
            int? clientId = await GetClientId();
            var estimate = (await _context.EstimateRepository.GetAsync(x=>x.ClientJobId == clientId && x.Id == id)).FirstOrDefault();

            if (estimate == null)
            {
                return NotFound();
            }

            return estimate;
        }

        // PUT: api/Estimates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstimate(int id, Estimate estimate)
        {
            if (id != estimate.Id)
            {
                return BadRequest();
            }

            _context.EstimateRepository.Update(estimate);

            try
            {
                await _context.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstimateExists(id))
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

        // POST: api/Estimates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Estimate>> PostEstimate(Estimate estimate)
        {
            if (_context.EstimateRepository == null)
            {
                return Problem("Entity set 'Estimates'  is null.");
            }
            _context.EstimateRepository.Insert(estimate);
            await _context.SaveAsync();

            return CreatedAtAction("GetEstimate", new { id = estimate.Id }, estimate);
        }

        // DELETE: api/Estimates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstimate(int id)
        {
            if (_context.EstimateRepository == null)
            {
                return NotFound();
            }
            var estimate = await _context.EstimateRepository.GetByIdAsync(id);
            if (estimate == null)
            {
                return NotFound();
            }

            _context.EstimateRepository.Delete(estimate);
            await _context.SaveAsync();

            return NoContent();
        }

        private bool EstimateExists(int id)
        {
            return _context.EstimateRepository.Exist(id);
        }

        private async Task<int?> GetClientId()
        {
            var userName = User?.Identity?.Name;
            var companyId = (await _context.UserRegisteredRepository.GetAsync(x => x.Name == userName)).FirstOrDefault()?.CompanyId;
            var clientId = (await _context.ClientJobRepository.GetAsync(x => x.CompanyId == companyId)).FirstOrDefault()?.Id;
            return clientId;
        }
    }
}
