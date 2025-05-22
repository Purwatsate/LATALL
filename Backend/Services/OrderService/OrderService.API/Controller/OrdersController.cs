using InventoryService.Grpc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using OrderService.InfraStructure.Data;

namespace OrderService.API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class OrdersController(OrderDbContext context, Inventory.InventoryClient inventoryClient) : ControllerBase
    {
        private readonly OrderDbContext _context = context;
        private readonly Inventory.InventoryClient _inventoryClient = inventoryClient;


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        [HttpGet("StockCount/{id}")]
        public async Task<ActionResult> GetTestStockCount(string id)
        {
            var response = await _inventoryClient.GetStockAsync(new StockRequest { ProductId = id });
            return Ok(response.Quantity);
        }

        [HttpPost("StockBatch")]
        public async Task<ActionResult<IEnumerable<StockResponse>>> GetStockBatch([FromBody] List<string> productIds)
        {
            if (productIds == null || productIds.Count == 0)
            {
                return BadRequest("ProductIds list cannot be empty.");
            }

            var request = new StockBatchRequest();
            request.ProductIds.AddRange(productIds);

            var response = await _inventoryClient.GetStockBatchAsync(request);

            return Ok(response.Stocks);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(Guid id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
