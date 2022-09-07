using KF.Common.Model.Automapper;

namespace KF.WebApi
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureApplicationServices(this IServiceCollection services)
        {
            new DependencyRegistration().Register(services);
            AutoMapperConfiguration.Init();
            AutoMapperConfiguration.MapperConfiguration.AssertConfigurationIsValid();
        }
    }
}
