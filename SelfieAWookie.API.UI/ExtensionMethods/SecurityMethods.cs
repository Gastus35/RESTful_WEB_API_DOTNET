using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SelfieAWookies.Core.Selfies.Infrastructures.Configurations;

namespace SelfieAWookie.API.UI.ExtensionMethods
{
    public static class SecurityMethods
    {
        #region Constants
        public const string DEFAULT_POLICY = "DEFAULT_POLICY";
        public const string DEFAULT_POLICY2 = "DEFAULT_POLICY2";
        #endregion

        #region Public methods
        public static void AddCustomSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            AddCustomCors(services, configuration);
            AddCustomAuthentication(services, configuration);
        }

        public static void AddCustomCors(this IServiceCollection services, IConfiguration configuration)
        {
            CorsOptions corsOptions = new CorsOptions();
            configuration.GetSection("Cors").Bind(corsOptions);
            services.AddCors(options =>
            {
                options.AddPolicy(DEFAULT_POLICY, builder =>
                {
                    builder.WithOrigins(corsOptions.Origin)
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
                options.AddPolicy(DEFAULT_POLICY2, builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
        }

        public static void AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            SecurityOptions securityOption = new SecurityOptions();
            configuration.GetSection("Jwt").Bind(securityOption);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                string maClef = securityOption.Key;
                options.SaveToken = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(maClef)),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateActor = false,
                    ValidateLifetime = false
                };
            });
        }
        #endregion
    }
}
