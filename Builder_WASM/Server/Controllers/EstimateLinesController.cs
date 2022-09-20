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

        // GET: api/EstimateLines/estimate/5
        [HttpGet("estimate/{id}")]
        public async Task<ActionResult<IEnumerable<EstimateLine>>> GetEstimateLines(int id)
        {
            if (_context.EstimateLineRepository == null)
            {
                return NotFound(new { message = "Repository not found" });
            }

            var result = await _context.EstimateLineRepository.GetAsync(x => x.EstimateId == id) ;

            return Ok(result);
        }

        // GET: api/EstimateLines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstimateLine>> GetEstimateLine(int id)
        {
            if (_context.EstimateLineRepository == null)
            {
                return NotFound(new { message = "Repository not found" });
            }
            var estimateLine = await _context.EstimateLineRepository.GetByIdAsync(id);

            if (estimateLine == null)
            {
                return NotFound(new { message = "Item not found" });
            }

            return Ok(estimateLine);
        }

        // PUT: api/EstimateLines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutEstimateLine(int id, EstimateLine estimateLine)
        {
            if (id != estimateLine.Id)
            {
                return BadRequest(new { message = "Item not found" });
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
                    return NotFound(new { message = "Item not found" });
                }
                else
                {
                    return BadRequest(new { message = "Error <Put>. Try later..." });
                }
            }

            return Ok(new { message = "Your action is successful" });
        }

        // POST: api/EstimateLines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EstimateLine>> PostEstimateLine(EstimateLine estimateLine)
        {
            if (_context.EstimateLineRepository == null)
            {
                return NotFound(new { message = "Repository not found" });
            }
            _context.EstimateLineRepository.Insert(estimateLine);
            await _context.SaveAsync();

            return CreatedAtAction(nameof(GetEstimateLine), new { id = estimateLine.Id }, estimateLine);
        }

        // DELETE: api/EstimateLines/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEstimateLine(int id)
        {
            if (_context.EstimateLineRepository == null)
            {
                return NotFound(new { message = "Repository not found" });
            }
            var estimateLine = await _context.EstimateLineRepository.GetByIdAsync(id);
            if (estimateLine == null)
            {
                return NotFound(new { message = "Item not found" });
            }

            _context.EstimateLineRepository.Delete(estimateLine);
            await _context.SaveAsync();

            return Ok(new { message = "Your action is successful" });
        }




        private bool EstimateLineExists(int id)
        {
            return _context.EstimateLineRepository.Exist(id);
        }

        
    }
}
