using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;

using Bulletin.Server.Models;
using Bulletin.Server.Models.Options;
using Bulletin.Server.Services;

using static Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults;

namespace Bulletin.Server;

public static class IServiceCollectionExtensions
{
    private static readonly ILogger _logger =
        Log.Logger.ForContext("SourceContext", nameof(IServiceCollectionExtensions));

    public static IServiceCollection AddBulletinOptions(this IServiceCollection services, IConfiguration config)
    {
        _logger.Verbose("Loading configuration options...");
        var bulletinConfig = config.GetSection(BulletinOptions.Bulletin);
        services.Configure<BulletinOptions>(bulletinConfig);
        services.Configure<AuthOptions>(bulletinConfig.GetSection(AuthOptions.Auth));
        services.Configure<TokenOptions>(bulletinConfig.GetSection(TokenOptions.Token));
        _logger.Verbose("Options loaded!");
        return services;
    }

    public static IServiceCollection AddBulletinAuth(this IServiceCollection services)
    {
        _logger.Verbose("Configuring authentication services...");
        using var sp = services.BuildServiceProvider();
        var tokenOptions = sp.GetRequiredService<IOptions<TokenOptions>>().Value;
        var authOptions = sp.GetRequiredService<IOptions<AuthOptions>>().Value;

        services.AddAuthentication(AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.IncludeErrorDetails = true;
                o.TokenValidationParameters = new()
                {
                    ValidateAudience = true,
                    ValidAudience = tokenOptions.Audience,
                    ValidateIssuer = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = tokenOptions.SecurityKey,
                };
            });

        _logger.Verbose("Authentication services configured!");
        _logger.Verbose("Configuring authorization services");
        services.AddAuthorization()
            .AddCors(o => o.AddDefaultPolicy(b => b
                .AllowCredentials()
                .AllowAnyHeader()
                .WithOrigins(authOptions.CorsOrigins.ToArray())))
            .AddScoped<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddScoped<ITokenService, TokenService>();

        _logger.Verbose("Authorization services configured!");
        return services;
    }

    public static IServiceCollection AddBulletinDatabase(this IServiceCollection services, IConfiguration config)
    {
        _logger.Verbose("Configuring Postgres database...");

        var connectionString = config.GetConnectionString("postgres");
        services.AddPooledDbContextFactory<DataContext>(o =>
            o.UseNpgsql(connectionString, x =>
                x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

        _logger.Verbose("Database configured!");
        return services;
    }

    public static IServiceCollection AddBulletinGraphQL(this IServiceCollection services)
    {
        _logger.Verbose("Configuring GraphQL request executor...");
        services.AddHttpContextAccessor()
            .AddGraphQLServer(Schema.DefaultName)
            .ConfigureGraphQL();

        _logger.Verbose("GraphQL request executor configured!");
        return services;
    }
}