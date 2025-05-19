using InventoryService.Domain.Entities;
using InventoryService.InfraStructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class ReservedStocksController(InventoryDbContext context) : ControllerBase
    {
        private readonly InventoryDbContext _context = context;

        // GET: api/ReservedStocks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservedStock>>> GetReservedStocks()
        {
            return await _context.ReservedStocks.ToListAsync();
        }

        // GET: api/ReservedStocks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservedStock>> GetReservedStock(Guid id)
        {
            var reservedStock = await _context.ReservedStocks.FindAsync(id);

            if (reservedStock == null)
            {
                return NotFound();
            }

            return reservedStock;
        }

        // PUT: api/ReservedStocks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservedStock(Guid id, ReservedStock reservedStock)
        {
            if (id != reservedStock.Id)
            {
                return BadRequest();
            }

            _context.Entry(reservedStock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservedStockExists(id))
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

        // POST: api/ReservedStocks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReservedStock>> PostReservedStock(ReservedStock reservedStock)
        {
            _context.ReservedStocks.Add(reservedStock);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservedStock", new { id = reservedStock.Id }, reservedStock);
        }

        // DELETE: api/ReservedStocks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservedStock(Guid id)
        {
            var reservedStock = await _context.ReservedStocks.FindAsync(id);
            if (reservedStock == null)
            {
                return NotFound();
            }

            _context.ReservedStocks.Remove(reservedStock);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservedStockExists(Guid id)
        {
            return _context.ReservedStocks.Any(e => e.Id == id);
        }
    }
}
