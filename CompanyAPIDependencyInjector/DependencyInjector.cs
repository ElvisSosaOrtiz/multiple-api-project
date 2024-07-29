namespace CompanyAPIDependencyInjector
{
    using Microsoft.Extensions.DependencyInjection;
    using ServiceContracts;
    using Services;

    public static class DependencyInjector
    {
        public static IServiceCollection InjectServices(this IServiceCollection services)
        {
            return services
                .AddScoped<ICompanyAPIService, CompanyAPIService>();
        }
    }
}
