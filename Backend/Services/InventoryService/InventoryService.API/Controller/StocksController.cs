using InventoryService.Contracts.Repository;
using InventoryService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class StocksController(IStockRepository stockRepository) : ControllerBase
    {
        private readonly IStockRepository _stockRepository = stockRepository;

        // GET: api/Stocks  
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stock>>> GetStocks()
        {
            var stocks = await _stockRepository.GetAllAsync();
            return Ok(stocks);
        }

        // GET: api/Stocks/5  
        [HttpGet("{id}")]
        public async Task<ActionResult<Stock>> GetStock(Guid id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock);
        }

        // PUT: api/Stocks/5  
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStock(Guid id, Stock stock)
        {
            if (id != stock.Id)
            {
                return BadRequest();
            }

            try
            {
                await _stockRepository.UpdateAsync(stock);
            }
            catch (Exception)
            {
                if (!await _stockRepository.ExistsAsync(id))
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

        // POST: api/Stocks  
        [HttpPost]
        public async Task<ActionResult<Stock>> PostStock(Stock stock)
        {
            await _stockRepository.AddAsync(stock);
            return CreatedAtAction(nameof(GetStock), new { id = stock.Id }, stock);
        }

        // DELETE: api/Stocks/5  
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(Guid id)
        {
            if (!await _stockRepository.ExistsAsync(id))
            {
                return NotFound();
            }

            await _stockRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
