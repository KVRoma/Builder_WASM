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
    public class PaymentsController : ControllerBase
    {
        private readonly IUnitOfWork _context;

        public PaymentsController(IUnitOfWork context)
        {
            _context = context;
        }

        // GET: api/Payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
        {
            if (_context.PaymentRepository == null)
            {
                return NotFound(new {message = "Repository not found!"});
            }
            int? id = await GetCompanyId();
            var result = await _context.PaymentRepository.GetAsync(x=>x.Estimate.ClientJob.CompanyId == id, includeProperties:"Estimate, ClientJob");
            return Ok(result);
        }

        // GET: api/Payments/estimate/5
        [HttpGet("estimate/{id}")]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments(int id)
        {
            if (_context.PaymentRepository == null)
            {
                return NotFound(new {message = "Repository Not found!"});
            }
            var payment = await _context.PaymentRepository.GetAsync(x=>x.EstimateId == id);

            if (payment == null)
            {
                return NotFound(new {message = "Item not found!"});
            }

            return Ok(payment);
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
            if (_context.PaymentRepository == null)
            {
                return NotFound(new {message = "Repository not found!"});
            }
            var payment = await _context.PaymentRepository.GetByIdAsync(id);

            if (payment == null)
            {
                return NotFound(new { message = "Item not found!"});
            }

            return Ok(payment);
        }

        // PUT: api/Payments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutPayment(int id, Payment payment)
        {
            if (id != payment.Id)
            {
                return BadRequest(new {message = "Item not found!"});
            }

            _context.PaymentRepository.Update(payment);

            try
            {
                await _context.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(id))
                {
                    return NotFound(new {message = "Item not found!"});
                }
                else
                {
                    return BadRequest(new { message = "Error <Put>. Try later..." });
                }
            }

            return Ok(new { message = "Your action is successful" });
        }

        // POST: api/Payments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Payment>> PostPayment(Payment payment)
        {
            if (_context.PaymentRepository == null)
            {
                return NotFound(new {message = "Repository not found!"});
            }
            _context.PaymentRepository.Insert(payment);
            await _context.SaveAsync();
            await EstimateCalculate(payment.EstimateId);
            return CreatedAtAction(nameof(GetPayment), new { id = payment.Id }, payment);
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePayment(int id)
        {
            if (_context.PaymentRepository == null)
            {
                return NotFound(new {message = "Repository not found!"});
            }
            var payment = await _context.PaymentRepository.GetByIdAsync(id);
            if (payment == null)
            {
                return NotFound(new {message = "Item not found!"});
            }

            _context.PaymentRepository.Delete(payment);
            await _context.SaveAsync();
            await EstimateCalculate(payment.EstimateId);
            return Ok(new { message = "Your action is successful" });
        }





        private bool PaymentExists(int id)
        {
            return _context.PaymentRepository.Exist(id);
        }

        private async Task<int?> GetCompanyId()
        {
            var userName = User?.Identity?.Name;
            var id = (await _context.UserRegisteredRepository.GetAsync(x => x.Name == userName)).FirstOrDefault()?.CompanyId;

            return id;
        }

        private async Task EstimateCalculate(int id)
        {
            var estimate = (await _context.EstimateRepository.GetAsync(x => x.Id == id, includeProperties: "Payments")).FirstOrDefault();
            estimate!.TotalPayment = estimate.Payments?.Select(x => x.TotalPayment)?.Sum() ?? 0m;
            _context.EstimateRepository.Update(estimate);
            await _context.SaveAsync();
        }
    }
}
