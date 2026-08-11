using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TaskFlow.Infrastructure.Identity;

namespace TaskFlowAPI.Configurations;

public static class AuthenticationConfiguration
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

        var jwtOptions = GetJwtOptions(configuration);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                IssuerSigningKey = CreateSigningKey(jwtOptions)
            };
        });

        services.AddAuthorization();

        return services;
    }
    private static JwtOptions GetJwtOptions(IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSection("Jwt").Get<JwtOptions>();

        if (jwtOptions == null)
        {
            throw new InvalidOperationException("JWT configuration section is missing or invalid.");
        }

        EnsureValidJwtOptions(jwtOptions);

        return jwtOptions;
    }

    private static void EnsureValidJwtOptions(JwtOptions jwtOptions)
    {
        if (string.IsNullOrEmpty(jwtOptions.Key))
        {
            throw new InvalidOperationException("JWT Key is missing or empty.");
        }
        if (string.IsNullOrEmpty(jwtOptions.Issuer))
        {
            throw new InvalidOperationException("JWT Issuer is missing or empty.");
        }
        if (string.IsNullOrEmpty(jwtOptions.Audience))
        {
            throw new InvalidOperationException("JWT Audience is missing or empty.");
        }
    }

    private static SymmetricSecurityKey CreateSigningKey(JwtOptions jwtOptions)
    {
        return new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtOptions.Key));
    }

}