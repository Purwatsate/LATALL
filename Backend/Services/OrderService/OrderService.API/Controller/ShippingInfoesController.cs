using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using OrderService.InfraStructure.Data;

namespace OrderService.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingInfoesController : ControllerBase
    {
        private readonly OrderDbContext _context;

        public ShippingInfoesController(OrderDbContext context)
        {
            _context = context;
        }

        // GET: api/ShippingInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShippingInfo>>> GetShippingInfos()
        {
            return await _context.ShippingInfos.ToListAsync();
        }

        // GET: api/ShippingInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShippingInfo>> GetShippingInfo(Guid id)
        {
            var shippingInfo = await _context.ShippingInfos.FindAsync(id);

            if (shippingInfo == null)
            {
                return NotFound();
            }

            return shippingInfo;
        }

        // PUT: api/ShippingInfoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShippingInfo(Guid id, ShippingInfo shippingInfo)
        {
            if (id != shippingInfo.Id)
            {
                return BadRequest();
            }

            _context.Entry(shippingInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShippingInfoExists(id))
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

        // POST: api/ShippingInfoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShippingInfo>> PostShippingInfo(ShippingInfo shippingInfo)
        {
            _context.ShippingInfos.Add(shippingInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShippingInfo", new { id = shippingInfo.Id }, shippingInfo);
        }

        // DELETE: api/ShippingInfoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShippingInfo(Guid id)
        {
            var shippingInfo = await _context.ShippingInfos.FindAsync(id);
            if (shippingInfo == null)
            {
                return NotFound();
            }

            _context.ShippingInfos.Remove(shippingInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShippingInfoExists(Guid id)
        {
            return _context.ShippingInfos.Any(e => e.Id == id);
        }
    }
}
