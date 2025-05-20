using Grpc.Core;
using InventoryService.Grpc;
using InventoryService.InfraStructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InventoryService.Application.Services.gRPC
{
    public class InventoryServiceImpl(InventoryDbContext context, ILogger<InventoryServiceImpl> logger) : Inventory.InventoryBase
    {
        private readonly InventoryDbContext _context = context;
        private readonly ILogger<InventoryServiceImpl> _logger = logger;

        public async override Task<StockResponse> GetStock(StockRequest request, ServerCallContext context)
        {
            _logger.LogInformation("GetStock GRPC {ProductId}", request.ProductId);
            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.ProductId == request.ProductId);
            var response = new StockResponse { ProductId = request.ProductId, Quantity = stock!.Quantity };
            return response;
        }

        public async override Task<StockBatchResponse> GetStockBatch(StockBatchRequest request, ServerCallContext context)
        {
            var response = new StockBatchResponse();

            var productIds = request.ProductIds.ToList();
            _logger.LogInformation("GetStockBatch GRPC with productIDs {ProductId}", productIds);
            var stocks = await _context.Stocks
                .Where(s => productIds.Contains(s.ProductId))
                .ToListAsync();

            foreach (var id in productIds)
            {
                var stock = stocks.FirstOrDefault(s => s.ProductId == id);
                response.Stocks.Add(new StockResponse
                {
                    ProductId = id,
                    Quantity = stock?.Quantity ?? 0
                });
            }

            return response;
        }


    }
}
