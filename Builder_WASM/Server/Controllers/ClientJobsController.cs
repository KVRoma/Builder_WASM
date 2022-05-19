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

namespace Builder_WASM.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientJobsController : ControllerBase
    {
        private readonly IUnitOfWork _context;

        public ClientJobsController(IUnitOfWork context)
        {
            _context = context;
        }

        // GET: api/ClientJobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientJob>>> GetClientJobs()
        {
            if (_context.ClientJobRepository == null)
            {
                return NotFound();
            }

            int? companyId = await GetCompanyId();
            var result = await _context.ClientJobRepository.GetAsync(x => x.CompanyId == companyId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET: api/ClientJobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientJob>> GetClientJob(int id)
        {
            if (_context.ClientJobRepository == null)
            {
                return NotFound();
            }

            int? companyId = await GetCompanyId();
            var clientJob = (await _context.ClientJobRepository.GetAsync(x=>x.Id == id && x.CompanyId == companyId)).FirstOrDefault();

            if (clientJob == null)
            {
                return NotFound();
            }

            return clientJob;
        }

        // PUT: api/ClientJobs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientJob(int id, ClientJob clientJob)
        {
            if (id != clientJob.Id)
            {
                return BadRequest();
            }

            _context.ClientJobRepository.Update(clientJob);

            try
            {
                await _context.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientJobExists(id))
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

        // POST: api/ClientJobs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClientJob>> PostClientJob(ClientJob clientJob)
        {
            if (_context.ClientJobRepository == null)
            {
                return Problem("Entity set 'ClientJobs'  is null.");
            }
            _context.ClientJobRepository.Insert(clientJob);
            await _context.SaveAsync();

            return CreatedAtAction("GetClientJob", new { id = clientJob.Id }, clientJob);
        }

        // DELETE: api/ClientJobs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientJob(int id)
        {
            if (_context.ClientJobRepository == null)
            {
                return NotFound();
            }
            var clientJob = await _context.ClientJobRepository.GetByIdAsync(id);
            if (clientJob == null)
            {
                return NotFound();
            }

            _context.ClientJobRepository.Delete(clientJob);
            await _context.SaveAsync();

            return NoContent();
        }

        private bool ClientJobExists(int id)
        {
            return _context.ClientJobRepository.Exist(id);
        }

        private async Task<int?> GetCompanyId()
        {
            var userName = User?.Identity?.Name;
            var id = (await _context.UserRegisteredRepository.GetAsync(x => x.Name == userName)).FirstOrDefault()?.CompanyId;
            
            return id;
        }
    }
}
