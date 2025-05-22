using InventoryService.Grpc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.DTOs;
using ProductService.Contracts.Repository;
using ProductService.Domain.Entities;
using System.Security.Claims;
using static InventoryService.Grpc.Inventory;

namespace ProductService.API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController(
        IProductRepository productRepo,
        InventoryClient inventoryClient,
        ILogger<ProductsController> logger) : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger = logger;
        private readonly IProductRepository _productRepo = productRepo;
        private readonly InventoryClient _inventoryClient = inventoryClient;

        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<ActionResult<List<ProductResponseDto>>> Get(
            [FromQuery] double? latitude,
            [FromQuery] double? longitude,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10
            )
        {
            var allProduct = await _productRepo.GetAllAsync(pageNumber, pageSize);
            if (allProduct.Count == 0)
                return NotFound();

            var productIds = allProduct
                .Where(p => !string.IsNullOrEmpty(p.Id))
                .Select(p => p.Id)
                .ToList();

            var stockInfos = await _inventoryClient.GetStockBatchAsync(new StockBatchRequest
            {
                ProductIds = { productIds },
                Latitude = latitude ?? 0.0,
                Longitude = longitude ?? 0.0
            });

            var stockInfoDict = stockInfos.Stocks.ToDictionary(s => s.ProductId);

            List<ProductResponseDto> ret = [];
            foreach (var p in allProduct)
            {
                stockInfoDict.TryGetValue(p.Id!, out var stockInfo);

                var productDto = new ProductResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    DiscountPrice = p.DiscountPrice,
                    Sku = p.Sku,
                    Quantity = stockInfo?.Quantity ?? 0,
                    WarehouseId = stockInfo?.WarehouseID
                };
                ret.Add(productDto);
            }
            return ret;
        }


        [Authorize(Roles = "User")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDto>> Get(string id, [FromQuery] double? latitude, [FromQuery] double? longitude)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product == null) return NotFound();

            var stockInfo = await _inventoryClient.GetStockAsync(new StockRequest
            {
                ProductId = id,
                Latitude = latitude ?? 0.0,
                Longitude = longitude ?? 0.0
            });

            var ret = new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                DiscountPrice = product.DiscountPrice,
                Sku = product.Sku,
                Quantity = stockInfo.Quantity,
                WarehouseId = stockInfo.WarehouseID
            };
            return Ok(ret);
        }

        [Authorize(Roles = "User")]
        [HttpGet("GetProductByCategory/{categoryId}")]
        public async Task<ActionResult<List<Product>>> GetProductByCategory(string categoryId)
        {
            return await _productRepo.GetProductByCategory(categoryId);
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

// Test to get nearest warehouse
//| Warehouse Name | Latitude | Longitude |
//| -------------------- | ----------- | ------------ |
//| Central Warehouse   | 20.59177 | 93.19149 |
//| Yangon Warehouse    | 16.8257 | 96.16242 |
//| Mandalay Warehouse  | 21.95053 | 96.09058 |
