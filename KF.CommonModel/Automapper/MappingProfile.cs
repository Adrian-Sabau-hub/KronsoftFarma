using AutoMapper;
using KF.CommonModel.Models;
using KF.Core.DomainModels;

namespace KF.Common.Model.Automapper
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Product, ProductModel>();
            CreateMap<ProductModel, Product>()
                .ForMember(s => s.Stock, d => d.Ignore());

            CreateMap<Stock, StockModel>();
            CreateMap<StockModel, Stock>();

            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
        }

    }
}
