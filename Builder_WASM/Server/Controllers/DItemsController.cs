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
    public class DItemsController : ControllerBase
    {
        private readonly IUnitOfWork _context;

        public DItemsController(IUnitOfWork context)
        {
            _context = context;
        }

        //// GET: api/DItems
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<DItem>>> GetDItems()
        //{
        //  if (_context.DItems == null)
        //  {
        //      return NotFound();
        //  }
        //    return await _context.DItems.ToListAsync();
        //}

        // GET: api/DItems/groupe/3
        [HttpGet("groupe/{id}")]
        public async Task<ActionResult<IEnumerable<DItem>>> GetDItems(int id)
        {
            if (_context.DItemRepository == null)
            {
                return NotFound(new { message = "Repository not found" });
            }
            var result = await _context.DItemRepository.GetAsync(x=>x.DGroupeId == id,includeProperties: "DGroupe,DSupplier");
            
            return Ok(result);
        }

        // GET: api/DItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DItem>> GetDItem(int id)
        {
          if (_context.DItemRepository == null)
          {
              return NotFound(new { message = "Repository not found" });
          }
            var dItem = await _context.DItemRepository.GetByIdAsync(id);

            if (dItem == null)
            {
                return NotFound(new { message = "Item not found" });
            }

            return Ok(dItem);
        }

        // PUT: api/DItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutDItem(int id, DItem dItem)
        {
            if (id != dItem.Id)
            {
                return BadRequest(new { message = "Item not found!" });
            }

            _context.DItemRepository.Update(dItem);

            try
            {
                await _context.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DItemExists(id))
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

        // POST: api/DItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DItem>> PostDItem(DItem dItem)
        {
          if (_context.DItemRepository == null)
          {
              return NotFound(new { message = "Repository not found" });
          }

            if (dItem.DGroupeId == 0)
            {
                return BadRequest(new { message = "Please indicate the Groupe!" });
            }

            _context.DItemRepository.Insert(dItem);
            await _context.SaveAsync();

            return CreatedAtAction(nameof(GetDItem), new { id = dItem.Id }, dItem);
        }

        // DELETE: api/DItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDItem(int id)
        {
            if (_context.DItemRepository == null)
            {
                return NotFound(new { message = "Repository not found" });
            }
            var dItem = await _context.DItemRepository.GetByIdAsync(id);
            if (dItem == null)
            {
                return NotFound(new { message = "Item not found" });
            }

            _context.DItemRepository.Delete(dItem);
            await _context.SaveAsync();

            return Ok(new { message = "Your action is successful" });
        }





        private bool DItemExists(int id)
        {
            return _context.DItemRepository.Exist(id);
        }
    }
}
