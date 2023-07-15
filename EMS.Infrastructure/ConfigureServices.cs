using EMS.Application.Repository.User;
using EMS.Infrastructure.DAL.Repositories.UserCommand;
using EMS.Infrastructure.DAL.Repositories.UserQuery;
using Microsoft.Extensions.DependencyInjection;

namespace EMS.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection  AddInfrastructureServices(this IServiceCollection services)
        {
            // Register dependencies
            services.AddTransient<IUserQueryRepository, UserQueryRepository>();
            services.AddTransient<IUserCommandRepository, UserCommandRepository>();
            return services;
        }
    }
}
