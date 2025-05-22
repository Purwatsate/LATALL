using Grpc.Core;
using InventoryService.Contracts.Repository;
using InventoryService.Domain.Entities;
using InventoryService.Domain.Utils;
using InventoryService.Grpc;
using Microsoft.Extensions.Logging;

namespace InventoryService.Application.Services.gRPC
{
    public class InventoryServiceImpl(
        IStockRepository repo,
        IWarehouseRepository warehouseRepository,
        ILogger<InventoryServiceImpl> logger
        ) : Inventory.InventoryBase
    {
        private readonly IStockRepository _stockRepo = repo;
        private readonly IWarehouseRepository _warehouseRepository = warehouseRepository;
        private readonly ILogger<InventoryServiceImpl> _logger = logger;

        public async override Task<StockResponse> GetStock(StockRequest request, ServerCallContext context)
        {
            _logger.LogInformation("--- GetStock GRPC {ProductId} ----", request.ProductId);
            var warehoueList = (await _warehouseRepository.GetAllAsync()).ToList();

            var nearestWarehouse = FindNearestWarehouse(request.Latitude, request.Longitude, warehoueList);

            var foundStock = await _stockRepo.GetByProductIdAndWarehouseIdAsync(request.ProductId, nearestWarehouse?.Id)
                ?? throw new RpcException(new Status(StatusCode.NotFound, $"Stock not found for ProductId: {request.ProductId}"));

            //finally
            var response = new StockResponse
            {
                ProductId = request.ProductId,
                Quantity = foundStock.Quantity,
                WarehouseID = nearestWarehouse.Id.ToString()
            };
            _logger.LogInformation("--- GetStock Finished {ProductId} ----", request.ProductId);
            return response;
        }

        public async override Task<StockBatchResponse> GetStockBatch(StockBatchRequest request, ServerCallContext context)
        {
            var response = new StockBatchResponse();

            _logger.LogInformation("--- GetStockBatch GRPC with productIDs {ProductId} ---", request.ProductIds);
            var warehoueList = (await _warehouseRepository.GetAllAsync()).ToList();

            var nearestWarehouse = FindNearestWarehouse(request.Latitude, request.Longitude, warehoueList) ?? throw new RpcException(new Status(StatusCode.NotFound, "No nearest warehouse found."));

            foreach (var id in request.ProductIds)
            {
                var stockTask = await _stockRepo.GetByProductIdAndWarehouseIdAsync(id, nearestWarehouse.Id);
                response.Stocks.Add(new StockResponse
                {
                    ProductId = id,
                    Quantity = stockTask?.Quantity ?? 0,
                    WarehouseID = stockTask?.WarehouseId.ToString()
                });
            }
            _logger.LogInformation("--- GetStockBatch Finished with productIDs {ProductId} ---", request.ProductIds);
            return response;
        }

        public Warehouse? FindNearestWarehouse(double customerLat, double customerLon, List<Warehouse> warehouses)
        {
            Warehouse? nearest = null;
            double shortestDistance = double.MaxValue;

            foreach (var warehouse in warehouses)
            {
                double distance = GeoUtils.CalculateDistanceKm(
                    customerLat, customerLon,
                    warehouse.Latitude, warehouse.Longitude
                );

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearest = warehouse;
                }
            }

            if (nearest == null)
                _logger.LogInformation("--- FindNearestWarehouse NotFound  ---");
            return nearest;
        }


    }
}
