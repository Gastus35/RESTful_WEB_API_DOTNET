using SelfieAWookies.Core.Selfies.Infrastructures.Configurations;

namespace SelfieAWookie.API.UI.ExtensionMethods
{
    public static class OptionsMethods
    {
        #region Public methods
        public static void AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SecurityOptions>(configuration.GetSection("Jwt"));
        }
        #endregion
    }
}
