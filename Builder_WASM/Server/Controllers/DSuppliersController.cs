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
    public class DSuppliersController : ControllerBase
    {
        private readonly IUnitOfWork _context;

        public DSuppliersController(IUnitOfWork context)
        {
            _context = context;
        }

        // GET: api/DSuppliers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DSupplier>>> GetDSuppliers()
        {
            if (_context.DSupplierRepository == null)
            {
                return NotFound();
            }
            int? companyId = await GetCompanyId();
            var result = await _context.DSupplierRepository.GetAsync(x => x.CompanyId == companyId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET: api/DSuppliers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DSupplier>> GetDSupplier(int? id)
        {
            if (_context.DSupplierRepository == null)
            {
                return NotFound();
            }
            int? companyId = await GetCompanyId();
            var dSupplier = (await _context.DSupplierRepository.GetAsync(x => x.Id == id && x.CompanyId == companyId)).FirstOrDefault();

            if (dSupplier == null)
            {
                return NotFound();
            }

            return dSupplier;
        }

        // PUT: api/DSuppliers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDSupplier(int? id, DSupplier dSupplier)
        {
            if (id != dSupplier.Id)
            {
                return BadRequest();
            }

            _context.DSupplierRepository.Update(dSupplier);

            try
            {
                await _context.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DSupplierExists(id))
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

        // POST: api/DSuppliers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DSupplier>> PostDSupplier(DSupplier dSupplier)
        {
            if (_context.DSupplierRepository == null)
            {
                return Problem("Entity set 'DSuppliers'  is null.");
            }
            _context.DSupplierRepository.Insert(dSupplier);
            await _context.SaveAsync();

            return CreatedAtAction("GetDSupplier", new { id = dSupplier.Id }, dSupplier);
        }

        // DELETE: api/DSuppliers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDSupplier(int? id)
        {
            if (_context.DSupplierRepository == null)
            {
                return NotFound();
            }
            var dSupplier = await _context.DSupplierRepository.GetByIdAsync(id!);
            if (dSupplier == null)
            {
                return NotFound();
            }

            _context.DSupplierRepository.Delete(dSupplier);
            await _context.SaveAsync();

            return NoContent();
        }

        private bool DSupplierExists(int? id)
        {
            return _context.DSupplierRepository.Exist(id!);
        }

        private async Task<int?> GetCompanyId()
        {
            var userName = User?.Identity?.Name;
            var id = (await _context.UserRegisteredRepository.GetAsync(x => x.Name == userName)).FirstOrDefault()?.CompanyId;

            return id;
        }
    }
}
