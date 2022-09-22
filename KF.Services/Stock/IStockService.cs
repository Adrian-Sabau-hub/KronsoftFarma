using KF.CommonModel.Models;

namespace KF.Services.Stock
{
    public interface IStockService
    {
        IEnumerable<StockModel> GetStocks();
        StockModel GetStockById(Guid stockId);
        StockModel GetStockByProductId(Guid productId);
        bool RemoveStockById(Guid stockId);
        StockModel CreateStock(StockModel stock);
        StockModel UpdateStock(StockModel stock);
    }
}
