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

namespace Builder_WASM.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
                return NotFound(new {message = "Repository not found!"});
            }

            int? companyId = await GetCompanyId();
            var result = await _context.ClientJobRepository.GetAsync(x => x.CompanyId == companyId);
            
            return Ok(result);
        }

        // GET: api/ClientJobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientJob>> GetClientJob(int id)
        {
            if (_context.ClientJobRepository == null)
            {
                return NotFound(new {message = "Repository not found!"});
            }

            int? companyId = await GetCompanyId();
            var clientJob = (await _context.ClientJobRepository.GetAsync(x=>x.Id == id && x.CompanyId == companyId)).FirstOrDefault();

            if (clientJob == null)
            {
                return NotFound(new {message = "Client not found!" });
            }

            return Ok(clientJob);
        }

        // PUT: api/ClientJobs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutClientJob(int id, ClientJob clientJob)
        {
            if (id != clientJob.Id)
            {
                return BadRequest(new {message = "Error <Put> client"});
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
                    return NotFound(new { message = "Client not found!"});
                }
                else
                {
                    return BadRequest(new { message = "Error <Put>. Try later..." });
                }
            }

            return Ok(new { message= "Your action is successful" });
        }

        // POST: api/ClientJobs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClientJob>> PostClientJob(ClientJob clientJob)
        {
            if (_context.ClientJobRepository == null)
            {
                return NotFound(new {message = "Repository not found!"});
            }

            clientJob.CompanyId = await GetCompanyId();
            if (clientJob.CompanyId == 0)
            {
                return BadRequest(new { message = "You are not registered with any company!" });
            }

            _context.ClientJobRepository.Insert(clientJob);
            await _context.SaveAsync();

            return CreatedAtAction(nameof(GetClientJob), new { id = clientJob.Id }, clientJob);
        }

        // DELETE: api/ClientJobs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClientJob(int id)
        {
            if (_context.ClientJobRepository == null)
            {
                return NotFound(new {message = "Repository not found!"});
            }
            var clientJob = await _context.ClientJobRepository.GetByIdAsync(id);
            if (clientJob == null)
            {
                return NotFound(new { message = "Client not found!"});
            }

            _context.ClientJobRepository.Delete(clientJob);
            await _context.SaveAsync();

            return Ok(new { message= "Your action is successful" });
        }




        private bool ClientJobExists(int id)
        {
            return _context.ClientJobRepository.Exist(id);
        }

        private async Task<int> GetCompanyId()
        {
            var userName = User?.Identity?.Name;
            var id = (await _context.UserRegisteredRepository.GetAsync(x => x.Name == userName)).First().CompanyId ?? 0;
            
            return id;
        }
               
    }
}
