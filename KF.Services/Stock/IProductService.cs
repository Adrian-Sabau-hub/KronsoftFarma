using KF.CommonModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KF.Services.Stock
{
    public interface IStockService
    {
        IEnumerable<StockModel> GetStocks();
        StockModel GetStockById(Guid stockId);
        bool RemoveStockById(Guid stockId);
        StockModel CreateStock(StockModel stock);
        StockModel UpdateStock(StockModel stock);
    }
}
