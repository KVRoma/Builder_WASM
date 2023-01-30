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
    public class DMethodPaymentsController : ControllerBase
    {
        private readonly IUnitOfWork _context;

        public DMethodPaymentsController(IUnitOfWork context)
        {
            _context = context;
        }

        // GET: api/DMethodPayments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DMethodPayment>>> GetDMethodPayments()
        {
            if (_context.DMethodPaymentRepository == null)
            {
                return NotFound(new { message = "Repository not found" });
            }
            int? companyId = await GetCompanyId();
            var result = await _context.DMethodPaymentRepository.GetAsync(x => x.CompanyId == companyId);
            if (result == null)
            {
                return NotFound(new { message = "Item not found" });
            }
            return Ok(result);
        }

        // GET: api/DMethodPayments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DMethodPayment>> GetDMethodPayment(int id)
        {
            if (_context.DMethodPaymentRepository == null)
            {
                return NotFound(new { message = "Repository not found" });
            }
            int? companyId = await GetCompanyId();
            var dMethodPayment = (await _context.DMethodPaymentRepository.GetAsync(x=>x.Id == id && x.CompanyId == companyId)).FirstOrDefault();

            if (dMethodPayment == null)
            {
                return NotFound(new { message = "Item not found" });
            }

            return Ok(dMethodPayment);
        }

        // PUT: api/DMethodPayments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutDMethodPayment(int id, DMethodPayment dMethodPayment)
        {
            if (id != dMethodPayment.Id)
            {
                return BadRequest(new { message = "Item not found" });
            }

            _context.DMethodPaymentRepository.Update(dMethodPayment);

            try
            {
                await _context.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DMethodPaymentExists(id))
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

        // POST: api/DMethodPayments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DMethodPayment>> PostDMethodPayment(DMethodPayment dMethodPayment)
        {
            if (_context.DMethodPaymentRepository == null)
            {
                return NotFound(new { message = "Repository not found" });
            }

            dMethodPayment.CompanyId = await GetCompanyId();
            if (dMethodPayment.CompanyId == 0)
            {
                return BadRequest(new { message = "You are not registered with any company!" });
            }

            _context.DMethodPaymentRepository.Insert(dMethodPayment);
            await _context.SaveAsync();

            return CreatedAtAction(nameof(GetDMethodPayment), new { id = dMethodPayment.Id }, dMethodPayment);
        }

        // DELETE: api/DMethodPayments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDMethodPayment(int id)
        {
            if (_context.DMethodPaymentRepository == null)
            {
                return NotFound(new { message = "Repository not found" });
            }
            var dMethodPayment = await _context.DMethodPaymentRepository.GetByIdAsync(id);
            if (dMethodPayment == null)
            {
                return NotFound(new { message = "Item not found" });
            }

            _context.DMethodPaymentRepository.Delete(dMethodPayment);
            await _context.SaveAsync();

            return Ok(new { message = "Your action is successful" });
        }




        private bool DMethodPaymentExists(int id)
        {
            return _context.DMethodPaymentRepository.Exist(id);
        }

        private async Task<int> GetCompanyId()
        {
            var userName = User?.Identity?.Name;
            var id = (await _context.UserRegisteredRepository.GetAsync(x => x.Name == userName)).FirstOrDefault()?.CompanyId ?? 0;

            return id;
        }
    }
}
