using KF.Common.Model.Automapper;
using KF.CommonModel.Models;
using KF.Core.Data;

namespace KF.Services.Stock
{
    public class StockService : IStockService
    {
        #region Fields
        private readonly IRepository<Core.DomainModels.Product> productRepository;
        private readonly IRepository<Core.DomainModels.Stock> stockRepository;

        #endregion

        #region ctor
        public StockService(IRepository<Core.DomainModels.Product> productRepository,
                              IRepository<Core.DomainModels.Stock> stockRepository
            )
        {
            this.productRepository = productRepository;
            this.stockRepository = stockRepository;
        }

        #endregion

        #region Methods

        public StockModel GetStockById(Guid stockId)
        {
            try
            {
                var stock = stockRepository.Table.FirstOrDefault(s => s.StockId == stockId);
                return stock.ToModel();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public StockModel GetStockByProductId(Guid productId)
        {
            try
            {
                var stock = stockRepository.Table.FirstOrDefault(s => s.ProductId == productId);
                return stock.ToModel();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public IEnumerable<StockModel> GetStocks()
        {
            try
            {
                var stocks = stockRepository.Table.Select(x => x.ToModel()).ToList();
                return stocks;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public StockModel CreateStock(StockModel stock)
        {
            try
            {
                if (stock == null)
                    throw new ArgumentNullException("Exception stock is null");

                KF.Core.DomainModels.Stock stockEntity = stock.ToEntity();
                stockRepository.Insert(stockEntity);

                StockModel createdStock = GetStockById(stockEntity.StockId);
                return createdStock;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public bool RemoveStockById(Guid stockId)
        {
            try
            {
                var stockEntity = stockRepository.Table.FirstOrDefault(x => x.StockId == stockId);

                if (stockEntity == null) return false;

                stockRepository.Delete(stockEntity);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public StockModel UpdateStock(StockModel stock)
        {
            try
            {
                if (stock == null)
                    throw new ArgumentNullException("Exception stock is null");

                var stockEntity = stockRepository.TableNoTracking.FirstOrDefault(s => s.StockId == stock.StockId);
                if (stockEntity == null) return null;

                stockRepository.Update(stock.ToEntity());
                return GetStockById(stock.StockId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        #endregion
    }
}
