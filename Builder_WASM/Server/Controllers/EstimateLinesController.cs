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
    public class EstimateLinesController : ControllerBase
    {
        private readonly IUnitOfWork _context;

        public EstimateLinesController(IUnitOfWork context)
        {
            _context = context;
        }

        // GET: api/EstimateLines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstimateLine>>> GetEstimateLines()
        {
            if (_context.EstimateLineRepository == null)
            {
                return NotFound();
            }
            var result = await _context.EstimateLineRepository.GetAsync();
            return Ok(result);
        }

        // GET: api/EstimateLines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstimateLine>> GetEstimateLine(int id)
        {
            if (_context.EstimateLineRepository == null)
            {
                return NotFound();
            }
            var estimateLine = await _context.EstimateLineRepository.GetByIdAsync(id);

            if (estimateLine == null)
            {
                return NotFound();
            }

            return estimateLine;
        }

        // PUT: api/EstimateLines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstimateLine(int id, EstimateLine estimateLine)
        {
            if (id != estimateLine.Id)
            {
                return BadRequest();
            }

            _context.EstimateLineRepository.Update(estimateLine);

            try
            {
                await _context.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstimateLineExists(id))
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

        // POST: api/EstimateLines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EstimateLine>> PostEstimateLine(EstimateLine estimateLine)
        {
            if (_context.EstimateLineRepository == null)
            {
                return Problem("Entity set 'EstimateLines'  is null.");
            }
            _context.EstimateLineRepository.Insert(estimateLine);
            await _context.SaveAsync();

            return CreatedAtAction("GetEstimateLine", new { id = estimateLine.Id }, estimateLine);
        }

        // DELETE: api/EstimateLines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstimateLine(int id)
        {
            if (_context.EstimateLineRepository == null)
            {
                return NotFound();
            }
            var estimateLine = await _context.EstimateLineRepository.GetByIdAsync(id);
            if (estimateLine == null)
            {
                return NotFound();
            }

            _context.EstimateLineRepository.Delete(estimateLine);
            await _context.SaveAsync();

            return NoContent();
        }

        private bool EstimateLineExists(int id)
        {
            return _context.EstimateLineRepository.Exist(id);
        }
    }
}
