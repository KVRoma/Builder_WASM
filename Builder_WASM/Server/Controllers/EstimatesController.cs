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
                return NotFound(new { message = "Repository not found" });
            }
            int id = await GetCompanyId();                       

            var result = await _context.EstimateRepository.GetAsync(x => x.ClientJob!.CompanyId == id, includeProperties: "ClientJob") ;
            if (result == null)
            {
                return NotFound(new { message = "Item not found" });
            }
            return Ok(result);
        }

        // GET: api/Estimates/client/5
        [HttpGet("client/{id}")]
        public async Task<ActionResult<IEnumerable<Estimate>>> GetEstimates(int id)
        {
            if (_context.EstimateRepository == null)
            {
                return NotFound(new { message = "Repository not found" });
            }
            
            var result = await _context.EstimateRepository.GetAsync(x => x.ClientJobId == id, includeProperties: "ClientJob");
            if (result == null)
            {
                return NotFound(new { message = "Item not found" });
            }
            return Ok(result);
        }

        // GET: api/Estimates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estimate>> GetEstimate(int id)
        {
            if (_context.EstimateRepository == null)
            {
                return NotFound(new { message = "Repository not found" });
            }
           
            var estimate = (await _context.EstimateRepository.GetAsync(x => x.Id == id)).FirstOrDefault();

            if (estimate == null)
            {
                return NotFound(new {message = "Item Not found!"});
            }

            return Ok(estimate);
        }

        // PUT: api/Estimates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutEstimate(int id, Estimate estimate)
        {
            if (id != estimate.Id)
            {
                return BadRequest(new { message = "Item not found!"});
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
                    return NotFound(new { message = "Item not found!"});
                }
                else
                {
                    return BadRequest(new { message = "Error <Put>. Try later..." });
                }
            }

            return Ok(new { message = "Your action is successful" });
        }

        // POST: api/Estimates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Estimate>> PostEstimate(Estimate estimate)
        {
            if (_context.EstimateRepository == null)
            {
                return NotFound(new { message = "Repository Not found!" });
            }

            if (estimate.ClientJobId == 0)
            {
                return BadRequest(new { message = "Please indicate the client!" });
            }
            
            int idCompany = await GetCompanyId();
            var comp = await _context.CompanyRepository.GetByIdAsync(idCompany);
            estimate.TAXpercent = comp.TAXpercent;
            estimate.GSTpercent = comp.GSTpercent;
            
            _context.EstimateRepository.Insert(estimate);
            await _context.SaveAsync();

            return CreatedAtAction(nameof(GetEstimate), new { id = estimate.Id }, estimate);
        }

        // DELETE: api/Estimates/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEstimate(int id)
        {
            if (_context.EstimateRepository == null)
            {
                return NotFound(new {message = "Repository not found!"});
            }
            var estimate = await _context.EstimateRepository.GetByIdAsync(id);
            if (estimate == null)
            {
                return NotFound(new {message = "Item not found!"});
            }

            _context.EstimateRepository.Delete(estimate);
            await _context.SaveAsync();

            return Ok(new { message = "Your action is successful" });
        }




        private bool EstimateExists(int id)
        {
            return _context.EstimateRepository.Exist(id);
        }

        private async Task<int> GetCompanyId()
        {
            var userName = User?.Identity?.Name;
            var id = (await _context.UserRegisteredRepository.GetAsync(x => x.Name == userName))?.FirstOrDefault()?.CompanyId ?? 0;

            return id;
        }
               
    }
}
