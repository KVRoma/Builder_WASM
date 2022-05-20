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
    public class DDescriptionsController : ControllerBase
    {
        private readonly IUnitOfWork _context;

        public DDescriptionsController(IUnitOfWork context)
        {
            _context = context;
        }

        //// GET: api/DDescriptions
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<DDescription>>> GetDDescriptions()
        //{
        //    if (_context.DDescriptions == null)
        //    {
        //        return NotFound();
        //    }
        //    return await _context.DDescriptions.ToListAsync();
        //}
        // GET: api/DDescriptions/item/5

        [HttpGet("item/{id}")]
        public async Task<ActionResult<IEnumerable<DDescription>>> GetDDescriptions(int id)
        {
            if (_context.DDescriptionRepository == null)
            {
                return NotFound();
            }
            var result = await _context.DDescriptionRepository.GetAsync(x=>x.DItemId == id);
            return Ok(result);
        }

        // GET: api/DDescriptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DDescription>> GetDDescription(int id)
        {
          if (_context.DDescriptionRepository == null)
          {
              return NotFound();
          }
            var dDescription = await _context.DDescriptionRepository.GetByIdAsync(id);

            if (dDescription == null)
            {
                return NotFound();
            }

            return dDescription;
        }

        // PUT: api/DDescriptions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDDescription(int id, DDescription dDescription)
        {
            if (id != dDescription.Id)
            {
                return BadRequest();
            }

            _context.DDescriptionRepository.Update(dDescription);

            try
            {
                await _context.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DDescriptionExists(id))
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

        // POST: api/DDescriptions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DDescription>> PostDDescription(DDescription dDescription)
        {
          if (_context.DDescriptionRepository == null)
          {
              return Problem("Entity set 'DDescriptions'  is null.");
          }
            _context.DDescriptionRepository.Insert(dDescription);
            await _context.SaveAsync();

            return CreatedAtAction("GetDDescription", new { id = dDescription.Id }, dDescription);
        }

        // DELETE: api/DDescriptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDDescription(int id)
        {
            if (_context.DDescriptionRepository == null)
            {
                return NotFound();
            }
            var dDescription = await _context.DDescriptionRepository.GetByIdAsync(id);
            if (dDescription == null)
            {
                return NotFound();
            }

            _context.DDescriptionRepository.Delete(dDescription);
            await _context.SaveAsync();

            return NoContent();
        }

        private bool DDescriptionExists(int id)
        {
            return _context.DDescriptionRepository.Exist(id);
        }
    }
}
