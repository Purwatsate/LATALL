
using InventoryService.Domain.Entities;
using InventoryService.InfraStructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class InventoryTransactionsController(InventoryDbContext context) : ControllerBase
    {
        private readonly InventoryDbContext _context = context;

        // GET: api/InventoryTransactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryTransaction>>> GetInventoryTransactions()
        {
            return await _context.InventoryTransactions.ToListAsync();
        }

        // GET: api/InventoryTransactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryTransaction>> GetInventoryTransaction(Guid id)
        {
            var inventoryTransaction = await _context.InventoryTransactions.FindAsync(id);

            if (inventoryTransaction == null)
            {
                return NotFound();
            }

            return inventoryTransaction;
        }

        // PUT: api/InventoryTransactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventoryTransaction(Guid id, InventoryTransaction inventoryTransaction)
        {
            if (id != inventoryTransaction.Id)
            {
                return BadRequest();
            }

            _context.Entry(inventoryTransaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryTransactionExists(id))
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

        // POST: api/InventoryTransactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InventoryTransaction>> PostInventoryTransaction(InventoryTransaction inventoryTransaction)
        {
            _context.InventoryTransactions.Add(inventoryTransaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventoryTransaction", new { id = inventoryTransaction.Id }, inventoryTransaction);
        }

        // DELETE: api/InventoryTransactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryTransaction(Guid id)
        {
            var inventoryTransaction = await _context.InventoryTransactions.FindAsync(id);
            if (inventoryTransaction == null)
            {
                return NotFound();
            }

            _context.InventoryTransactions.Remove(inventoryTransaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventoryTransactionExists(Guid id)
        {
            return _context.InventoryTransactions.Any(e => e.Id == id);
        }
    }
}
