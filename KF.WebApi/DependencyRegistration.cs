using FluentValidation;
using KF.Common.Model.Models;
using KF.Common.Model.Models.Validators;
using KF.CommonModel.Models;
using KF.CommonModel.Models.Validators;
using KF.Core.Data;
using KF.Services.Product;
using KF.Services.Stock;
using KF.Services.User;

namespace KF.WebApi
{
    public class DependencyRegistration
    {
        /// <summary>
        /// Register services
        /// </summary>
        /// <param name="builder"></param>
        public void Register(IServiceCollection builder)
        {
            //Per request lifetime

            //Singleton services

            //Transient services

            builder.AddTransient(typeof(IRepository<>), typeof(EFCoreRepository<>));
            builder.AddTransient<IProductService, ProductService>();
            builder.AddTransient<IStockService, StockService>();
            builder.AddTransient<IUserService, UserService>();

            builder.AddTransient<IValidator<ProductModel>, ProductModelValidator>();
            builder.AddTransient<IValidator<StockModel>, StockModelValidator>();
            builder.AddTransient<IValidator<UserModel>, UserModelValidator>();

        }

    }
}
