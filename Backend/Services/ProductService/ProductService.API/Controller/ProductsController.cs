using InventoryService.Grpc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.DTOs;
using ProductService.Domain.Entities;
using ProductService.InfraStructure.Repository;
using System.Security.Claims;
using static InventoryService.Grpc.Inventory;

namespace ProductService.API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController(
        ProductRepository productRepo,
        InventoryClient inventoryClient,
        ILogger<ProductsController> logger) : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger = logger;
        private readonly ProductRepository _productRepo = productRepo;
        private readonly InventoryClient _inventoryClient = inventoryClient;

        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            //await _productRepo.InsertManyAsync();
            return await _productRepo.GetAllAsync();
        }

        [Authorize(Roles = "User")]
        [HttpGet("GetProductByCategory/{categoryId}")]
        public async Task<ActionResult<List<Product>>> GetProductByCategory(string categoryId)
        {
            return await _productRepo.GetProductByCategory(categoryId);
        }


        [Authorize(Roles = "User")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(string id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product == null) return NotFound();
            var stockInfo = await _inventoryClient.GetStockAsync(new StockRequest { ProductId = id });
            var ret = new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                DiscountPrice = product.DiscountPrice,
                Sku = product.Sku,
                Quantity = stockInfo.Quantity
            };
            return Ok(ret);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            product.CreatedBy = userId!;
            await _productRepo.CreateAsync(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Product updated)
        {
            var existing = await _productRepo.GetByIdAsync(id);
            if (existing == null) return NotFound();

            updated.Id = id; // ensure ID is correct  
            await _productRepo.UpdateAsync(id, updated);
            return NoContent();
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _productRepo.GetByIdAsync(id);
            if (existing == null) return NotFound();

            await _productRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}
