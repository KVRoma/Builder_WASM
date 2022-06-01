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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IUnitOfWork _context;

        public CompaniesController(IUnitOfWork context)
        {
            _context = context;
        }

        // GET: api/Companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
          if (_context.CompanyRepository == null)
          {
              return NotFound();
          }            
            var result = await _context.CompanyRepository.GetAsync();
            return Ok(result);
        }

        // GET: api/Companies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
          if (_context.CompanyRepository == null)
          {
              return NotFound();
          }
            var company = await _context.CompanyRepository.GetByIdAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }

        // PUT: api/Companies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, Company company)
        {
            if (id != company.Id)
            {
                return BadRequest();
            }

            _context.CompanyRepository.Update(company);

            try
            {
                await _context.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
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

        // POST: api/Companies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany(Company company)
        {
          if (_context.CompanyRepository == null)
          {
              return Problem("Entity set 'Companies'  is null.");
          }
            _context.CompanyRepository.Insert(company);
            await _context.SaveAsync();

            return CreatedAtAction("GetCompany", new { id = company.Id }, company);
        }

        // DELETE: api/Companies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            if (_context.CompanyRepository == null)
            {
                return NotFound();
            }
            //var company = await _context.CompanyRepository.GetByIdAsync(id);
            var company = await _context.CompanyRepository.GetAsync(x=>x.Id == id,includeProperties: "UserRegistered");
            if (company == null)
            {
                return NotFound();
            }

            _context.CompanyRepository.Delete(company);
            await _context.SaveAsync();

            return Ok(new { message = "The company deleted successfully!" });
        }

        private bool CompanyExists(int id)
        {
            return (_context.CompanyRepository.Exist(id));
        }
    }
}
