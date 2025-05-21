using InventoryService.Contracts.Repository;
using InventoryService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class WarehousesController(IWarehouseRepository repository) : ControllerBase
    {
        private readonly IWarehouseRepository _repository = repository;

        // GET: api/Warehouses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Warehouse>>> GetWarehouses()
        {
            var warehouses = await _repository.GetAllAsync();
            return Ok(warehouses);
        }

        // GET: api/Warehouses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Warehouse>> GetWarehouse(Guid id)
        {
            var warehouse = await _repository.GetByIdAsync(id);

            if (warehouse == null)
            {
                return NotFound();
            }

            return Ok(warehouse);
        }

        // PUT: api/Warehouses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarehouse(Guid id, Warehouse warehouse)
        {
            if (id != warehouse.Id)
            {
                return BadRequest();
            }

            if (!await _repository.ExistsAsync(id))
            {
                return NotFound();
            }

            await _repository.UpdateAsync(warehouse);

            return NoContent();
        }

        // POST: api/Warehouses
        [HttpPost]
        public async Task<ActionResult<Warehouse>> PostWarehouse(Warehouse warehouse)
        {
            await _repository.AddAsync(warehouse);

            return CreatedAtAction(nameof(GetWarehouse), new { id = warehouse.Id }, warehouse);
        }

        // DELETE: api/Warehouses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarehouse(Guid id)
        {
            if (!await _repository.ExistsAsync(id))
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}
