using SelfieAWookie.Core.Selfies.Domain;
using SelfieAWookies.Core.Selfies.Infrastructures.Repositories;
using MediatR;

namespace SelfieAWookie.API.UI.ExtensionMethods
{
    public static class DIMethods
    {
        #region Public methods
        public static IServiceCollection AddInjections(this IServiceCollection services)
        {
            services.AddScoped<ISelfieRepository, DefaultSelfieRepository>();
            services.AddMediatR(typeof(Program));

            return services;
        }
        #endregion
    }
}
